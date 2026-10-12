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

    /// <summary>
    /// The average time per output - total runtime divided by the number of outputs produced.
    /// Null when the simulator has not produced any results yet.
    /// </summary>
    public TimeSpan? OutputFrequency { get; }

    /// <summary>
    /// True when the simulator has been silent for noticeably longer than its own normal output
    /// cadence (see <see cref="OutputFrequency"/>). Simulators without an established cadence
    /// (fewer than two results) are never considered stale.
    /// </summary>
    public bool IsStale { get; }

    /// <summary>True when the simulator has produced at least one result.</summary>
    public bool HasResults => Results.Count > 0;

    /// <summary>
    /// All results, ordered newest-first. The "latest only" view is applied client-side (it hides
    /// all but the first row), so the full set is always rendered.
    /// </summary>
    public IReadOnlyList<SimulatorResult> Results { get; }

    /// <summary>The newest result, or null when none exist.</summary>
    public SimulatorResult? Latest => Results.Count > 0 ? Results[0] : null;

    /// <summary>A short, tab-friendly label derived from the simulator id.</summary>
    public string ShortId => Id.ToString()[..8];

    public SimulatorViewModel(Simulator simulator, DateTime asOfUtc, double staleMultiplier)
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

        OutputFrequency = CalculateOutputFrequency(Uptime, ordered.Count);
        IsStale = CalculateIsStale(OutputFrequency, TimeSinceLastOutput, staleMultiplier);

        Results = ordered;
    }

    /// <summary>
    /// The average time per output - i.e. how often the simulator produces a result on average,
    /// measured as the total runtime divided by the number of outputs produced. Returns null when
    /// no results exist yet.
    /// </summary>
    private static TimeSpan? CalculateOutputFrequency(TimeSpan uptime, int resultCount)
    {
        if (resultCount < 1)
        {
            return null;
        }

        return uptime / resultCount;
    }

    /// <summary>
    /// Stale when the simulator is overdue for a new result by more than the configured margin.
    /// <para>
    /// The next output is expected roughly one <paramref name="outputFrequency"/> after the last
    /// one, so we give credit for that pending output: a simulator sitting in a normal gap waiting
    /// for its next result is not stale. Only once the silence exceeds the expected interval by
    /// the multiplier - i.e. <c>(multiplier + 1) x frequency</c> - is it flagged stale.
    /// Without an established cadence (fewer than two results), the simulator is not stale.
    /// </para>
    /// </summary>
    private static bool CalculateIsStale(TimeSpan? outputFrequency, TimeSpan? timeSinceLastOutput, double staleMultiplier)
    {
        if (outputFrequency is null || timeSinceLastOutput is null)
        {
            return false;
        }

        // Discount the one output that is expected but has not arrived yet.
        var overdueBy = timeSinceLastOutput.Value - outputFrequency.Value;

        return overdueBy > outputFrequency.Value * staleMultiplier;
    }

    private static TimeSpan Max(TimeSpan a, TimeSpan b)
    {
        return a > b ? a : b;
    }
}
