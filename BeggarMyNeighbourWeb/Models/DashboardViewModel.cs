namespace BeggarMyNeighbourWeb.Models;

/// <summary>
/// The dashboard: all known simulators (one tab each) plus the display toggle.
/// </summary>
public class DashboardViewModel
{
    /// <summary>Simulators to show, one per tab.</summary>
    public IReadOnlyList<SimulatorViewModel> Simulators { get; init; } = [];

    /// <summary>
    /// When true, each tab shows only the latest result; otherwise it shows all results,
    /// newest first.
    /// </summary>
    public bool LatestOnly { get; init; }

    /// <summary>The UTC instant the data was captured; all derived durations are relative to this.</summary>
    public DateTime AsOfUtc { get; init; }

    /// <summary>
    /// Number of simulators that answered the discovery poll over the message bus on this load.
    /// This reflects live connectivity, independent of the results shown in the tabs.
    /// </summary>
    public int ConnectedSimulatorCount { get; init; }

    /// <summary>True when there are no simulators to display.</summary>
    public bool IsEmpty => Simulators.Count == 0;
}
