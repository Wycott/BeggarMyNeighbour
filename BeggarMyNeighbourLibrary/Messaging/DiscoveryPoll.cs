namespace BeggarMyNeighbourLibrary.Messaging;

/// <summary>
/// A discovery request broadcast by the web application. Simulators that receive it reply with
/// a <see cref="DiscoveryResponse"/> carrying the same <see cref="PollId"/> so the web
/// application can correlate replies to this specific poll and ignore stale ones.
/// </summary>
public class DiscoveryPoll
{
    /// <summary>Unique id for this poll. Responders echo it back in their response.</summary>
    public Guid PollId { get; set; }

    /// <summary>UTC time the poll was issued (useful for logging / diagnostics).</summary>
    public DateTime IssuedUtc { get; set; }
}
