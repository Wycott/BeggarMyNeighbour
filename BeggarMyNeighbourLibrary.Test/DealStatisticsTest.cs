using AiAnnotations;

namespace BeggarMyNeighbourLibrary.Test;

[AiGenerated]
public class DealStatisticsTest
{
    [Fact]
    public void Properties_DefaultToZeroAndEmpty()
    {
        var stats = new DealStatistics();
        
        Assert.Equal(0, stats.Cards);
        Assert.Equal(0, stats.Tricks);
        Assert.Equal(string.Empty, stats.PlayerOneOutcome);
        Assert.Equal(string.Empty, stats.PlayerTwoOutcome);
    }

    [Fact]
    public void Properties_CanBeSet()
    {
        var stats = new DealStatistics
        {
            Cards = 100,
            Tricks = 10,
            PlayerOneOutcome = "Win",
            PlayerTwoOutcome = "Lose"
        };
        
        Assert.Equal(100, stats.Cards);
        Assert.Equal(10, stats.Tricks);
        Assert.Equal("Win", stats.PlayerOneOutcome);
        Assert.Equal("Lose", stats.PlayerTwoOutcome);
    }
}
