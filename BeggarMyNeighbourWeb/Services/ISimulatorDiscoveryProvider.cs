namespace BeggarMyNeighbourWeb.Services;

/// <summary>Identity of a simulator that answered a discovery poll.</summary>
public record DiscoveredSimulator(Guid SimulatorId, DateTime StartedUtc);

/// <summary>
/// Discovers which simulators are currently connected by polling them over the message bus.
/// </summary>
public interface ISimulatorDiscoveryProvider
{
    /// <summary>
    /// Publishes a discovery poll and collects the responses that arrive within the configured
    /// window. Returns the distinct simulators that replied.
    /// </summary>
    Task<IReadOnlyList<DiscoveredSimulator>> DiscoverAsync(CancellationToken cancellationToken = default);
}
