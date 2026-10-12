using System.Collections.Concurrent;
using BeggarMyNeighbourLibrary;
using BeggarMyNeighbourLibrary.Messaging;
using Microsoft.Extensions.Options;
using MQTTnet;
using MQTTnet.Protocol;

namespace BeggarMyNeighbourWeb.Services;

/// <summary>
/// Pulls simulator results live over MQTT (Mosquitto). Each call opens a short-lived connection,
/// subscribes to the results-response topic, publishes a results request, gathers the replies
/// that arrive within the configured window, then tears the connection down. This matches the
/// "pull on page load / refresh" model: a request runs per dashboard load, not continuously.
/// </summary>
public sealed class MqttSimulatorResultsProvider : ISimulatorResultsProvider
{
    private readonly MqttSettings _mqtt;
    private readonly DiscoverySettings _discovery;
    private readonly ILogger<MqttSimulatorResultsProvider> _logger;

    public MqttSimulatorResultsProvider(
        IOptions<MqttSettings> mqtt,
        IOptions<DiscoverySettings> discovery,
        ILogger<MqttSimulatorResultsProvider> logger)
    {
        _mqtt = mqtt.Value;
        _discovery = discovery.Value;
        _logger = logger;
    }

    public async Task<IReadOnlyList<Simulator>> GetSimulatorsAsync(CancellationToken cancellationToken = default)
    {
        var requestId = Guid.NewGuid();

        // Keyed by simulator id so a simulator replying more than once is only kept once
        // (last reply wins).
        var responses = new ConcurrentDictionary<Guid, ResultsResponse>();

        var factory = new MqttClientFactory();
        using var client = factory.CreateMqttClient();

        client.ApplicationMessageReceivedAsync += e =>
        {
            if (e.ApplicationMessage.Topic == DiscoveryTopics.ResultsResponse)
            {
                var response = MessageSerializer.Deserialize<ResultsResponse>(e.ApplicationMessage.Payload);

                if (response is not null && response.RequestId == requestId)
                {
                    responses[response.SimulatorId] = response;
                }
            }

            return Task.CompletedTask;
        };

        try
        {
            var options = new MqttClientOptionsBuilder()
                .WithTcpServer(_mqtt.Host, _mqtt.Port)
                .WithClientId($"bmn-web-results-{requestId:N}")
                .WithCleanSession()
                .Build();

            await client.ConnectAsync(options, cancellationToken);

            // Subscribe before publishing so we don't miss fast responders.
            var subscribeOptions = new MqttClientSubscribeOptionsBuilder()
                .WithTopicFilter(f => f.WithTopic(DiscoveryTopics.ResultsResponse).WithAtMostOnceQoS())
                .Build();

            await client.SubscribeAsync(subscribeOptions, cancellationToken);

            var request = new ResultsRequest
            {
                RequestId = requestId,
                IssuedUtc = DateTime.UtcNow,
            };

            var requestMessage = new MqttApplicationMessageBuilder()
                .WithTopic(DiscoveryTopics.ResultsRequest)
                .WithPayload(MessageSerializer.Serialize(request))
                .WithQualityOfServiceLevel(MqttQualityOfServiceLevel.AtMostOnce)
                .Build();

            await client.PublishAsync(requestMessage, cancellationToken);

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
            // Broker unreachable or similar: treat as "no simulators" rather than failing the
            // page. The dashboard still renders (empty).
            _logger.LogWarning(ex, "Simulator results pull failed against {Host}:{Port}", _mqtt.Host, _mqtt.Port);
            return [];
        }

        // Materialise each response into a Simulator, rehydrated with its reported id/start time.
        return responses.Values
            .OrderBy(r => r.StartedUtc)
            .Select(ToSimulator)
            .ToList();
    }

    private static Simulator ToSimulator(ResultsResponse response)
    {
        var simulator = new Simulator(response.SimulatorId, response.StartedUtc);

        foreach (var result in response.Results)
        {
            simulator.AddResult(result);
        }

        return simulator;
    }
}
