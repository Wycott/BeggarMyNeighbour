using BeggarMyNeighbourLibrary.Helpers;

namespace BeggarMyNeighbourLibrary.Test;

public class DeckTest
{
    [Fact]
    public void Generate_CreatesAFullDeck()
    {
        const int FullDeckSize = 52;

        var res = Deck.Generate();

        Assert.Equal(FullDeckSize, res.Count);
    }

    [Fact]
    public void GenerateStacks_CreatesTwoStacksOfCards()
    {
        const int ExpectedLength = 26;

        var deal = Deck.GenerateStacks();

        Assert.Equal(ExpectedLength, deal.PlayerOneCards.Count);
        Assert.Equal(ExpectedLength, deal.PlayerTwoCards.Count);
        Assert.Equal(ExpectedLength, deal.PlayerOneDeal.Length);
        Assert.Equal(ExpectedLength, deal.PlayerTwoDeal.Length);
    }
}