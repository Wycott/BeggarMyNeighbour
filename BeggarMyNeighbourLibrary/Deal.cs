namespace BeggarMyNeighbourLibrary
{
    public class Deal
    {
        public List<Card> PlayerOneCards { get; set; } = new List<Card>();
        public List<Card> PlayerTwoCards { get; set; } = new List<Card>();
        public string PlayerOneDeal { get; set; } = string.Empty;
        public string PlayerTwoDeal { get; set; } = string.Empty;
    }
}
