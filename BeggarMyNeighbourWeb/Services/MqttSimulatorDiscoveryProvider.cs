using System.Collections.Concurrent;
using BeggarMyNeighbourLibrary.Messaging;
using Microsoft.Extensions.Options;
using MQTTnet;
using MQTTnet.Protocol;

namespace BeggarMyNeighbourWeb.Services;

/// <summary>
/// Discovers connected simulators over MQTT (Mosquitto). Each call opens a short-lived
/// connection, subscribes to the response topic, publishes a poll, gathers the replies that
/// arrive within the configured window, then tears the connection down. This matches the
/// "pull on page load / refresh" model: discovery runs per request, not continuously.
/// </summary>
public sealed class MqttSimulatorDiscoveryProvider : ISimulatorDiscoveryProvider
{
    private readonly MqttSettings _mqtt;
    private readonly DiscoverySettings _discovery;
    private readonly ILogger<MqttSimulatorDiscoveryProvider> _logger;

    public MqttSimulatorDiscoveryProvider(
        IOptions<MqttSettings> mqtt,
        IOptions<DiscoverySettings> discovery,
        ILogger<MqttSimulatorDiscoveryProvider> logger)
    {
        _mqtt = mqtt.Value;
        _discovery = discovery.Value;
        _logger = logger;
    }

    public async Task<IReadOnlyList<DiscoveredSimulator>> DiscoverAsync(CancellationToken cancellationToken = default)
    {
        var pollId = Guid.NewGuid();

        // Keyed by simulator id so a simulator replying more than once is only counted once.
        var responders = new ConcurrentDictionary<Guid, DiscoveredSimulator>();

        var factory = new MqttClientFactory();
        using var client = factory.CreateMqttClient();

        client.ApplicationMessageReceivedAsync += e =>
        {
            if (e.ApplicationMessage.Topic == DiscoveryTopics.Response)
            {
                var response = MessageSerializer.Deserialize<DiscoveryResponse>(e.ApplicationMessage.Payload);

                // Only count replies to the poll we just issued, ignoring any stragglers from
                // a previous poll still in flight.
                if (response is not null && response.PollId == pollId)
                {
                    responders[response.SimulatorId] =
                        new DiscoveredSimulator(response.SimulatorId, response.StartedUtc);
                }
            }

            return Task.CompletedTask;
        };

        try
        {
            var options = new MqttClientOptionsBuilder()
                .WithTcpServer(_mqtt.Host, _mqtt.Port)
                .WithClientId($"bmn-web-{pollId:N}")
                .WithCleanSession()
                .Build();

            await client.ConnectAsync(options, cancellationToken);

            // Subscribe before publishing so we don't miss fast responders.
            var subscribeOptions = new MqttClientSubscribeOptionsBuilder()
                .WithTopicFilter(f => f.WithTopic(DiscoveryTopics.Response).WithAtMostOnceQoS())
                .Build();

            await client.SubscribeAsync(subscribeOptions, cancellationToken);

            var poll = new DiscoveryPoll
            {
                PollId = pollId,
                IssuedUtc = DateTime.UtcNow,
            };

            var pollMessage = new MqttApplicationMessageBuilder()
                .WithTopic(DiscoveryTopics.Poll)
                .WithPayload(MessageSerializer.Serialize(poll))
                .WithQualityOfServiceLevel(MqttQualityOfServiceLevel.AtMostOnce)
                .Build();

            await client.PublishAsync(pollMessage, cancellationToken);

            // Wait the configured window for simulators to answer.
            await Task.Delay(_discovery.ResponseWindow, cancellationToken);

            if (client.IsConnected)
            {
                await client.DisconnectAsync();
            }
        }
        catch (OperationCanceledException)
        {
            throw;
        }
        catch (Exception ex)
        {
            // Broker unreachable or similar: treat as "no simulators discovered" rather than
            // failing the page. The dashboard still renders.
            _logger.LogWarning(ex, "Simulator discovery failed against {Host}:{Port}", _mqtt.Host, _mqtt.Port);
            return [];
        }

        return responders.Values
            .OrderBy(s => s.StartedUtc)
            .ToList();
    }
}
