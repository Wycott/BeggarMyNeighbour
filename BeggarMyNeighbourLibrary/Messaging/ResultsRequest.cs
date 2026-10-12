namespace BeggarMyNeighbourLibrary.Messaging;

/// <summary>
/// A request broadcast by the web application asking every simulator to report its current
/// results. Simulators reply with a <see cref="ResultsResponse"/> echoing the
/// <see cref="RequestId"/> so the web application can correlate replies to this request.
/// </summary>
public class ResultsRequest
{
    /// <summary>Unique id for this request. Responders echo it back in their response.</summary>
    public Guid RequestId { get; set; }

    /// <summary>UTC time the request was issued (useful for logging / diagnostics).</summary>
    public DateTime IssuedUtc { get; set; }
}
