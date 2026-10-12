using BeggarMyNeighbourLibrary;

namespace BeggarMyNeighbourWeb.Services;

/// <summary>
/// Supplies the current set of simulators and their recorded results to the web application.
/// The data is pulled live from the simulators over the message bus each time the dashboard is
/// loaded or refreshed.
/// </summary>
public interface ISimulatorResultsProvider
{
    /// <summary>
    /// Gets a snapshot of all known simulators and their results as of the moment of the call.
    /// </summary>
    Task<IReadOnlyList<Simulator>> GetSimulatorsAsync(CancellationToken cancellationToken = default);
}
