namespace BeggarMyNeighbourWeb.Services;

/// <summary>
/// Controls how long the web application waits for simulators to answer a discovery poll.
/// Bound from the "Discovery" section of appsettings.json.
/// </summary>
public class DiscoverySettings
{
    public const string SectionName = "Discovery";

    /// <summary>
    /// The maximum time to collect responses after publishing a request, in milliseconds. Acts as
    /// a hard safety cap: collection returns earlier once responses go quiet (see
    /// <see cref="IdleGapMilliseconds"/>). Default 2000ms.
    /// </summary>
    public int ResponseWindowMilliseconds { get; set; } = 2000;

    /// <summary>
    /// Collection returns early once no new response has arrived for this long, in milliseconds.
    /// Should sit comfortably above a simulator's reply jitter (0-250ms) plus broker round-trip so
    /// that a brief lull between genuine responders is not mistaken for "everyone has answered".
    /// Default 400ms.
    /// </summary>
    public int IdleGapMilliseconds { get; set; } = 400;

    public TimeSpan ResponseWindow => TimeSpan.FromMilliseconds(ResponseWindowMilliseconds);

    public TimeSpan IdleGap => TimeSpan.FromMilliseconds(IdleGapMilliseconds);
}
