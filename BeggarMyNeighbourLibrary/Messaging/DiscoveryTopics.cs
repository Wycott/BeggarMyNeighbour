namespace BeggarMyNeighbourLibrary.Messaging;

/// <summary>
/// MQTT topic names shared by the web application (publisher of polls) and the simulators
/// (responders). Keeping these in one place avoids the classic MQTT bug where a publisher
/// and subscriber disagree on the topic string and silently never talk.
/// </summary>
public static class DiscoveryTopics
{
    /// <summary>
    /// Topic the web application publishes a <see cref="DiscoveryPoll"/> to. Every simulator
    /// subscribes to this and replies.
    /// </summary>
    public const string Poll = "bmn/discovery/poll";

    /// <summary>
    /// Topic simulators publish their <see cref="DiscoveryResponse"/> to, and that the web
    /// application subscribes to while collecting replies.
    /// </summary>
    public const string Response = "bmn/discovery/response";

    /// <summary>
    /// Topic the web application publishes a <see cref="ResultsRequest"/> to. Simulators reply
    /// with their current results.
    /// </summary>
    public const string ResultsRequest = "bmn/results/request";

    /// <summary>
    /// Topic simulators publish their <see cref="ResultsResponse"/> to, and that the web
    /// application subscribes to while collecting results.
    /// </summary>
    public const string ResultsResponse = "bmn/results/response";
}
