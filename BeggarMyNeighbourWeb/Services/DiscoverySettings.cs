namespace BeggarMyNeighbourWeb.Services;

/// <summary>
/// Controls how long the web application waits for simulators to answer a discovery poll.
/// Bound from the "Discovery" section of appsettings.json.
/// </summary>
public class DiscoverySettings
{
    public const string SectionName = "Discovery";

    /// <summary>
    /// How long to collect responses after publishing a poll, in milliseconds. Needs to comfortably
    /// exceed a simulator's reply jitter plus broker round-trip. Default 2000ms.
    /// </summary>
    public int ResponseWindowMilliseconds { get; set; } = 2000;

    public TimeSpan ResponseWindow => TimeSpan.FromMilliseconds(ResponseWindowMilliseconds);
}
