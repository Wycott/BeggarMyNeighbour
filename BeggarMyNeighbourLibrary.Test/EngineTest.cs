namespace BeggarMyNeighbourLibrary.Test;

public class EngineTest
{
    [Fact]
    public async void RunScenario_LongestRun_ReturnsExpectedTricksAndCards()
    {
        const int ExpectedTricks = 1164;
        const int ExpectedCards = 8344;

        const string PlayerOneDeal = "---AJ--Q---------QAKQJJ-QK";
        const string PlayerTwoDeal = "-----A----KJ-K--------A---";

        var res = await Engine.RunScenario(PlayerOneDeal, PlayerTwoDeal);

        Assert.Equal(ExpectedTricks, res.Tricks);
        Assert.Equal(ExpectedCards, res.Cards);
    }

    [Fact]
    public async void RunScenario_2ndLongestRun_ReturnsExpectedTricksAndCards()
    {
        const int ExpectedTricks = 1106;
        const int ExpectedCards = 7972;
            
        const string PlayerOneDeal = "----K---A--Q-A--JJA------J";
        const string PlayerTwoDeal = "-----KK---------A-JK-Q-Q-Q";

        var res = await Engine.RunScenario(PlayerOneDeal, PlayerTwoDeal);

        Assert.Equal(ExpectedTricks, res.Tricks);
        Assert.Equal(ExpectedCards, res.Cards);
    }
}