namespace BeggarMyNeighbourLibrary.Test;

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
}