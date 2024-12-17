using BeggarMyNeighbourLibrary.Helpers;

namespace BeggarMyNeighbourLibrary.Test;

public class DeckTest
{
    [Fact]
    public async void Test1()
    {
        const int FullDeckSize = 52;

        var res = await Deck.Generate();

        Assert.Equal(FullDeckSize, res.Count);
    }

    [Fact]
    public async void Test2()
    {
        const int ExpectedLength = 26;

        var deal = await Deck.GenerateStacks();

        Assert.Equal(ExpectedLength, deal.PlayerOneCards.Count);
        Assert.Equal(ExpectedLength, deal.PlayerTwoCards.Count);
        Assert.Equal(ExpectedLength, deal.PlayerOneDeal.Length);
        Assert.Equal(ExpectedLength, deal.PlayerTwoDeal.Length);
    }
}