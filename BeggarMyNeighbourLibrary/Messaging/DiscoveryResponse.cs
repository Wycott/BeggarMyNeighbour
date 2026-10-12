namespace BeggarMyNeighbourLibrary.Messaging;

/// <summary>
/// A simulator's reply to a <see cref="DiscoveryPoll"/>, announcing its identity. The web
/// application collects these during a poll window to learn how many simulators are connected.
/// </summary>
public class DiscoveryResponse
{
    /// <summary>The <see cref="DiscoveryPoll.PollId"/> this response is answering.</summary>
    public Guid PollId { get; set; }

    /// <summary>The responding simulator's unique identity.</summary>
    public Guid SimulatorId { get; set; }

    /// <summary>UTC time the responding simulator started.</summary>
    public DateTime StartedUtc { get; set; }
}
