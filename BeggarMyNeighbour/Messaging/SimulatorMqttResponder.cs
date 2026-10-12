using BeggarMyNeighbourLibrary;
using BeggarMyNeighbourLibrary.Messaging;
using MQTTnet;
using MQTTnet.Protocol;

namespace BeggarMyNeighbour.Messaging;

/// <summary>
/// Connects this simulator to the MQTT broker and answers requests from the web application:
/// discovery polls (replying with the simulator's identity) and results requests (replying with
/// the simulator's recorded results). Runs for the life of the process alongside the simulation
/// loop.
/// </summary>
internal sealed class SimulatorMqttResponder : IAsyncDisposable
{
    private readonly Simulator _simulator;
    private readonly MqttSettings _settings;
    private readonly Random _jitter = new();
    private readonly IMqttClient _client;

    public SimulatorMqttResponder(Simulator simulator, MqttSettings settings)
    {
        _simulator = simulator;
        _settings = settings;
        _client = new MqttClientFactory().CreateMqttClient();
        _client.ApplicationMessageReceivedAsync += OnMessageReceivedAsync;
    }

    /// <summary>
    /// Connects to the broker and subscribes to the request topics. Reconnects on demand if a
    /// publish later finds the connection dropped.
    /// </summary>
    public async Task StartAsync(CancellationToken cancellationToken = default)
    {
        await ConnectAsync(cancellationToken);

        var subscribeOptions = new MqttClientSubscribeOptionsBuilder()
            .WithTopicFilter(f => f.WithTopic(DiscoveryTopics.Poll).WithAtMostOnceQoS())
            .WithTopicFilter(f => f.WithTopic(DiscoveryTopics.ResultsRequest).WithAtMostOnceQoS())
            .Build();

        await _client.SubscribeAsync(subscribeOptions, cancellationToken);

        Console.WriteLine($"[mqtt] Listening for polls and results requests via {_settings.Host}:{_settings.Port}");
    }

    private async Task ConnectAsync(CancellationToken cancellationToken)
    {
        var options = new MqttClientOptionsBuilder()
            .WithTcpServer(_settings.Host, _settings.Port)
            // A stable, readable client id helps when inspecting the broker.
            .WithClientId($"bmn-sim-{_simulator.Id:N}")
            .WithCleanSession()
            .Build();

        await _client.ConnectAsync(options, cancellationToken);
    }

    private async Task OnMessageReceivedAsync(MqttApplicationMessageReceivedEventArgs e)
    {
        switch (e.ApplicationMessage.Topic)
        {
            case DiscoveryTopics.Poll:
                await HandlePollAsync(e);
                break;

            case DiscoveryTopics.ResultsRequest:
                await HandleResultsRequestAsync(e);
                break;
        }
    }

    private async Task HandlePollAsync(MqttApplicationMessageReceivedEventArgs e)
    {
        var poll = MessageSerializer.Deserialize<DiscoveryPoll>(e.ApplicationMessage.Payload);

        if (poll is null)
        {
            return;
        }

        // Small random delay so many simulators don't all answer in the same instant.
        await Task.Delay(_jitter.Next(0, 250));

        var response = new DiscoveryResponse
        {
            PollId = poll.PollId,
            SimulatorId = _simulator.Id,
            StartedUtc = _simulator.StartedUtc,
        };

        await PublishAsync(DiscoveryTopics.Response, MessageSerializer.Serialize(response), "discovery response");
    }

    private async Task HandleResultsRequestAsync(MqttApplicationMessageReceivedEventArgs e)
    {
        var request = MessageSerializer.Deserialize<ResultsRequest>(e.ApplicationMessage.Payload);

        if (request is null)
        {
            return;
        }

        // Small random delay to spread replies from many simulators.
        await Task.Delay(_jitter.Next(0, 250));

        var response = new ResultsResponse
        {
            RequestId = request.RequestId,
            SimulatorId = _simulator.Id,
            StartedUtc = _simulator.StartedUtc,
            // Simulator.Results returns a thread-safe snapshot copy.
            Results = _simulator.Results.ToList(),
        };

        await PublishAsync(DiscoveryTopics.ResultsResponse, MessageSerializer.Serialize(response), "results response");
    }

    private async Task PublishAsync(string topic, byte[] payload, string description)
    {
        var message = new MqttApplicationMessageBuilder()
            .WithTopic(topic)
            .WithPayload(payload)
            .WithQualityOfServiceLevel(MqttQualityOfServiceLevel.AtMostOnce)
            .Build();

        try
        {
            if (!_client.IsConnected)
            {
                await ConnectAsync(CancellationToken.None);
            }

            await _client.PublishAsync(message);
        }
        catch (Exception ex)
        {
            // Best-effort; never let a broker hiccup disturb the simulation loop.
            Console.WriteLine($"[mqtt] Failed to publish {description}: {ex.Message}");
        }
    }

    public async ValueTask DisposeAsync()
    {
        if (_client.IsConnected)
        {
            await _client.DisconnectAsync();
        }

        _client.Dispose();
    }
}
