using AiAnnotations;
using AiAnnotations.Types;
using BeggarMyNeighbourLibrary.Helpers;

namespace BeggarMyNeighbourLibrary.Test;

[AiGenerated(Authorship.Hybrid)]
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

    [Fact]
    public void CreateDealFromDeck_SplitsDeckIntoTwoHalves()
    {
        var deck = Deck.Generate();
        var deal = Deck.CreateDealFromDeck(deck);

        Assert.Equal(26, deal.PlayerOneCards.Count);
        Assert.Equal(26, deal.PlayerTwoCards.Count);
        Assert.Equal(26, deal.PlayerOneDeal.Length);
        Assert.Equal(26, deal.PlayerTwoDeal.Length);
    }

    [Fact]
    public void Generate_ContainsCorrectCardDistribution()
    {
        var deck = Deck.Generate();
        var aces = deck.Count(c => c.SimpleRank == 'A');
        var kings = deck.Count(c => c.SimpleRank == 'K');
        var queens = deck.Count(c => c.SimpleRank == 'Q');
        var jacks = deck.Count(c => c.SimpleRank == 'J');
        var others = deck.Count(c => c.SimpleRank == '-');

        Assert.Equal(4, aces);
        Assert.Equal(4, kings);
        Assert.Equal(4, queens);
        Assert.Equal(4, jacks);
        Assert.Equal(36, others);
    }
}