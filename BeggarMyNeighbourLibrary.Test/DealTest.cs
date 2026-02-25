using AiAnnotations;

namespace BeggarMyNeighbourLibrary.Test;

[AiGenerated]
public class DealTest
{
    [Fact]
    public void Properties_DefaultToEmptyCollections()
    {
        var deal = new Deal();
        
        Assert.Empty(deal.PlayerOneCards);
        Assert.Empty(deal.PlayerTwoCards);
        Assert.Equal(string.Empty, deal.PlayerOneDeal);
        Assert.Equal(string.Empty, deal.PlayerTwoDeal);
    }

    [Fact]
    public void Properties_CanBeSet()
    {
        var cards = new List<Card> { new('A'), new('K') };
        var deal = new Deal
        {
            PlayerOneCards = cards,
            PlayerTwoCards = cards,
            PlayerOneDeal = "AK",
            PlayerTwoDeal = "QJ"
        };
        
        Assert.Equal(2, deal.PlayerOneCards.Count);
        Assert.Equal(2, deal.PlayerTwoCards.Count);
        Assert.Equal("AK", deal.PlayerOneDeal);
        Assert.Equal("QJ", deal.PlayerTwoDeal);
    }
}
