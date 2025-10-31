namespace BeggarMyNeighbourLibrary;

public class Deal
{
    public List<Card> PlayerOneCards { get; set; } = [];
    public List<Card> PlayerTwoCards { get; set; } = [];
    public string PlayerOneDeal { get; set; } = string.Empty;
    public string PlayerTwoDeal { get; set; } = string.Empty;
}