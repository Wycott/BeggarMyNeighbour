using AiAnnotations;
using BeggarMyNeighbourLibrary.Helpers;

namespace BeggarMyNeighbourLibrary.Test;

[AiGenerated]
public class RuntimeFormatterTest
{
    [Theory]
    [InlineData(0, "Runtime 0 secs")]
    [InlineData(1, "Runtime 0 secs")]
    [InlineData(999, "Runtime 0 secs")]
    [InlineData(1000, "Runtime 1 sec")]
    [InlineData(1500, "Runtime 1 sec")]
    [InlineData(2000, "Runtime 2 secs")]
    [InlineData(59_000, "Runtime 59 secs")]
    public void Format_UsesSeconds_UnderOneMinute(long milliseconds, string expected)
    {
        Assert.Equal(expected, RuntimeFormatter.Format(milliseconds));
    }

    [Theory]
    [InlineData(60_000, "Runtime 1 min")]
    [InlineData(90_000, "Runtime 1 min")]
    [InlineData(120_000, "Runtime 2 mins")]
    [InlineData(3_540_000, "Runtime 59 mins")]
    public void Format_UsesMinutes_UnderOneHour(long milliseconds, string expected)
    {
        Assert.Equal(expected, RuntimeFormatter.Format(milliseconds));
    }

    [Theory]
    [InlineData(3_600_000, "Runtime 1 hour")]
    [InlineData(5_400_000, "Runtime 1 hour")]
    [InlineData(7_200_000, "Runtime 2 hours")]
    [InlineData(82_800_000, "Runtime 23 hours")]
    public void Format_UsesHours_UnderOneDay(long milliseconds, string expected)
    {
        Assert.Equal(expected, RuntimeFormatter.Format(milliseconds));
    }

    [Theory]
    [InlineData(86_400_000, "Runtime 1 day")]
    [InlineData(129_600_000, "Runtime 1 day")]
    [InlineData(172_800_000, "Runtime 2 days")]
    [InlineData(864_000_000, "Runtime 10 days")]
    public void Format_UsesDays_AtOrAboveOneDay(long milliseconds, string expected)
    {
        Assert.Equal(expected, RuntimeFormatter.Format(milliseconds));
    }

    [Fact]
    public void Format_FormatsLargeDayCount_WithGroupSeparators()
    {
        // 1000 days
        var milliseconds = 1000L * 86_400_000L;

        Assert.Equal("Runtime 1,000 days", RuntimeFormatter.Format(milliseconds));
    }

    [Fact]
    public void Format_TreatsNegativeAsZero()
    {
        Assert.Equal("Runtime 0 secs", RuntimeFormatter.Format(-5000));
    }
}
