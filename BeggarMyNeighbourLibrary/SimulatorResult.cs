namespace BeggarMyNeighbourLibrary;

/// <summary>
/// A structured snapshot of a "best result so far" produced by a simulator run.
/// Captured in a collection so it can later be returned to a web application.
/// </summary>
public class SimulatorResult
{
    /// <summary>Sequential record number for this result within a simulator run.</summary>
    public int Record { get; set; }

    /// <summary>UTC time this result was recorded.</summary>
    public DateTime RecordedUtc { get; set; }

    /// <summary>Elapsed runtime of the simulator, in milliseconds, when this result was recorded.</summary>
    public long RuntimeMilliseconds { get; set; }

    /// <summary>Total number of deals iterated so far.</summary>
    public long Iterations { get; set; }

    /// <summary>Iterations per second at the time of recording.</summary>
    public long Rate { get; set; }

    /// <summary>Best iterations-per-second rate observed so far.</summary>
    public long BestRate { get; set; }

    /// <summary>Highest number of cards played in a single game so far.</summary>
    public int Cards { get; set; }

    /// <summary>Number of full decks represented by <see cref="Cards"/>.</summary>
    public int Decks { get; set; }

    /// <summary>Highest number of tricks played in a single game so far.</summary>
    public int Tricks { get; set; }

    /// <summary>Player one's deal string.</summary>
    public string PlayerOneDeal { get; set; } = string.Empty;

    /// <summary>Player two's deal string.</summary>
    public string PlayerTwoDeal { get; set; } = string.Empty;

    /// <summary>Count of face cards in player one's deal.</summary>
    public int PlayerOneSuits { get; set; }

    /// <summary>Count of face cards in player two's deal.</summary>
    public int PlayerTwoSuits { get; set; }

    /// <summary>Player one's outcome string.</summary>
    public string PlayerOneOutcome { get; set; } = string.Empty;

    /// <summary>Player two's outcome string.</summary>
    public string PlayerTwoOutcome { get; set; } = string.Empty;

    /// <summary>The release code name of the simulator build that produced this result.</summary>
    public string ReleaseCodeName { get; set; } = string.Empty;
}
