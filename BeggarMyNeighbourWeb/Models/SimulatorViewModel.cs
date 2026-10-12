using BeggarMyNeighbourLibrary;

namespace BeggarMyNeighbourWeb.Models;

/// <summary>
/// A single simulator rendered as one tab on the dashboard, with derived timing values.
/// </summary>
public class SimulatorViewModel
{
    /// <summary>The simulator's unique identity.</summary>
    public Guid Id { get; }

    /// <summary>UTC time the simulator started.</summary>
    public DateTime StartedUtc { get; }

    /// <summary>How long the simulator has been running, as of the dashboard's as-of time.</summary>
    public TimeSpan Uptime { get; }

    /// <summary>
    /// How long since the simulator produced a new result, as of the dashboard's as-of time.
    /// Null when the simulator has not produced any results yet.
    /// </summary>
    public TimeSpan? TimeSinceLastOutput { get; }

    /// <summary>True when the simulator has produced at least one result.</summary>
    public bool HasResults => Results.Count > 0;

    /// <summary>
    /// Results to display, already ordered newest-first and reduced to the latest only when the
    /// dashboard is in "latest only" mode.
    /// </summary>
    public IReadOnlyList<SimulatorResult> Results { get; }

    /// <summary>The newest result, or null when none exist.</summary>
    public SimulatorResult? Latest => Results.Count > 0 ? Results[0] : null;

    /// <summary>A short, tab-friendly label derived from the simulator id.</summary>
    public string ShortId => Id.ToString()[..8];

    public SimulatorViewModel(Simulator simulator, bool latestOnly, DateTime asOfUtc)
    {
        ArgumentNullException.ThrowIfNull(simulator);

        Id = simulator.Id;
        StartedUtc = simulator.StartedUtc;
        Uptime = Max(asOfUtc - simulator.StartedUtc, TimeSpan.Zero);

        // Newest first.
        var ordered = simulator.Results
            .OrderByDescending(r => r.RecordedUtc)
            .ToList();

        if (ordered.Count > 0)
        {
            TimeSinceLastOutput = Max(asOfUtc - ordered[0].RecordedUtc, TimeSpan.Zero);
        }

        Results = latestOnly
            ? ordered.Take(1).ToList()
            : ordered;
    }

    private static TimeSpan Max(TimeSpan a, TimeSpan b)
    {
        return a > b ? a : b;
    }
}
