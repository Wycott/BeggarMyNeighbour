using AiAnnotations;
using AiAnnotations.Types;

namespace BeggarMyNeighbourLibrary.Test;

[AiGenerated(Authorship.Hybrid)]
public class EngineTest
{
    [Fact]
    public void RunScenario_LongestRun_ReturnsExpectedTricksAndCards()
    {
        const int ExpectedTricks = 1164;
        const int ExpectedCards = 8344;

        const string PlayerOneDeal = "---AJ--Q---------QAKQJJ-QK";
        const string PlayerTwoDeal = "-----A----KJ-K--------A---";

        var res = Engine.RunScenario(PlayerOneDeal, PlayerTwoDeal);

        Assert.Equal(ExpectedTricks, res.Tricks);
        Assert.Equal(ExpectedCards, res.Cards);
    }

    [Fact]
    public void RunScenario_2ndLongestRun_ReturnsExpectedTricksAndCards()
    {
        const int ExpectedTricks = 1106;
        const int ExpectedCards = 7972;

        const string PlayerOneDeal = "----K---A--Q-A--JJA------J";
        const string PlayerTwoDeal = "-----KK---------A-JK-Q-Q-Q";

        var res = Engine.RunScenario(PlayerOneDeal, PlayerTwoDeal);

        Assert.Equal(ExpectedTricks, res.Tricks);
        Assert.Equal(ExpectedCards, res.Cards);
    }

    [Fact]
    public void RunScenario_RandomOutput_ReturnsExpectedTricksAndCards()
    {
        const int ExpectedTricks = 432;
        const int ExpectedCards = 3180;

        const string PlayerOneDeal = "J-Q---K-K---K-----AA-A---A";
        const string PlayerTwoDeal = "Q--------J--QK-----Q-JJ---";

        var res = Engine.RunScenario(PlayerOneDeal, PlayerTwoDeal);

        Assert.Equal(ExpectedTricks, res.Tricks);
        Assert.Equal(ExpectedCards, res.Cards);
    }

    [Fact]
    public void RunScenario_PlayerOneWins_ReturnsCorrectOutcome()
    {
        const string PlayerOneDeal = "AAAA----------------------";
        const string PlayerTwoDeal = "--------------------------";

        var res = Engine.RunScenario(PlayerOneDeal, PlayerTwoDeal);

        Assert.Equal("Win", res.PlayerOneOutcome);
        Assert.Equal("Lose", res.PlayerTwoOutcome);
    }

    [Fact]
    public void RunScenario_PlayerTwoWins_ReturnsCorrectOutcome()
    {
        const string PlayerOneDeal = "--------------------------";
        const string PlayerTwoDeal = "AAAA----------------------";

        var res = Engine.RunScenario(PlayerOneDeal, PlayerTwoDeal);

        Assert.Equal("Lose", res.PlayerOneOutcome);
        Assert.Equal("Win", res.PlayerTwoOutcome);
    }

    [Fact]
    public void RunScenario_WithQueues_ReturnsStatistics()
    {
        var p1 = new Queue<Card>(new[] { new Card('A'), new Card('-') });
        var p2 = new Queue<Card>(new[] { new Card('-'), new Card('-') });

        var res = Engine.RunScenario(p1, p2);

        Assert.True(res.Cards > 0);
        Assert.True(res.Tricks > 0);
    }

    [Fact]
    public void RunScenario_ShortGame_ReturnsLowCardCount()
    {
        const string PlayerOneDeal = "A-------------------------";
        const string PlayerTwoDeal = "--------------------------";

        var res = Engine.RunScenario(PlayerOneDeal, PlayerTwoDeal);

        Assert.True(res.Cards < 100);
        Assert.True(res.Tricks > 0);
    }
}