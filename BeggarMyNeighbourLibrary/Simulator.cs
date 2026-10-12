namespace BeggarMyNeighbourLibrary;

/// <summary>
/// Represents a single running simulator instance. Each simulator assigns itself a
/// unique <see cref="Id"/> when it starts so it can identify itself to a web
/// application that queries multiple simulators (e.g. several running on a NAS).
/// Results are accumulated in <see cref="Results"/> for later retrieval.
/// </summary>
public class Simulator
{
    /// <summary>Unique identity assigned to this simulator when it starts.</summary>
    public Guid Id { get; }

    /// <summary>UTC time this simulator started.</summary>
    public DateTime StartedUtc { get; }

    /// <summary>
    /// A point-in-time snapshot of all result snapshots recorded by this simulator, in order.
    /// Returns a copy so callers can safely enumerate it while the simulation thread keeps
    /// adding results.
    /// </summary>
    public IReadOnlyList<SimulatorResult> Results
    {
        get
        {
            lock (_resultsLock)
            {
                return _results.ToList();
            }
        }
    }

    private readonly List<SimulatorResult> _results = [];
    private readonly Lock _resultsLock = new();

    /// <summary>
    /// Starts a brand-new simulator, assigning it a fresh identity and the current UTC time.
    /// Used by a running simulator process.
    /// </summary>
    public Simulator()
        : this(Guid.NewGuid(), DateTime.UtcNow)
    {
    }

    /// <summary>
    /// Rehydrates a simulator with a known identity and start time. Used when materialising a
    /// simulator from data received elsewhere (e.g. pulled over the message bus by the web app).
    /// </summary>
    public Simulator(Guid id, DateTime startedUtc)
    {
        Id = id;
        StartedUtc = startedUtc;
    }

    /// <summary>Records a new result snapshot against this simulator.</summary>
    public void AddResult(SimulatorResult result)
    {
        ArgumentNullException.ThrowIfNull(result);

        lock (_resultsLock)
        {
            _results.Add(result);
        }
    }
}
