namespace BeggarMyNeighbourLibrary.Messaging;

/// <summary>
/// A simulator's reply to a <see cref="ResultsRequest"/>, carrying its identity and the results
/// it has recorded so far. The web application uses these to populate the dashboard tabs.
/// </summary>
public class ResultsResponse
{
    /// <summary>The <see cref="ResultsRequest.RequestId"/> this response is answering.</summary>
    public Guid RequestId { get; set; }

    /// <summary>The responding simulator's unique identity.</summary>
    public Guid SimulatorId { get; set; }

    /// <summary>UTC time the responding simulator started.</summary>
    public DateTime StartedUtc { get; set; }

    /// <summary>The results the simulator has recorded so far.</summary>
    public List<SimulatorResult> Results { get; set; } = [];
}
