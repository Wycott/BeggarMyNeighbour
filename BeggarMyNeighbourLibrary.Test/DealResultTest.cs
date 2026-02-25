using AiAnnotations;
using AiAnnotations.Types;

namespace BeggarMyNeighbourLibrary.Test;

[AiGenerated(Authorship.Hybrid)]
public class DealResultTest
{
    [Fact]
    public void PropertiesAndMethods()
    {
        const int Tricks = 1164;
        const int Cards = 8344;

        const string PlayerOneDeal = "---AJ--Q---------QAKQJJ-QK";
        const string PlayerTwoDeal = "-----A----KJ-K--------A---";

        const int PlayerOneSuits = 11;
        const int PlayerTwoSuits = 5;

        const string PlayerOneOutcome = "Win";
        const string PlayerTwoOutcome = "Lose";

        var dealResult = new DealResult()
        {
            PlayerOneDeal = PlayerOneDeal,
            PlayerTwoDeal = PlayerTwoDeal,
            Cards = Cards,
            Tricks = Tricks,
            PlayerOneOutcome = PlayerOneOutcome,
            PlayerTwoOutcome = PlayerTwoOutcome
        };

        Assert.Equal(Tricks, dealResult.Tricks);
        Assert.Equal(Cards, dealResult.Cards);
        Assert.Equal(PlayerOneDeal, dealResult.PlayerOneDeal);
        Assert.Equal(PlayerTwoDeal, dealResult.PlayerTwoDeal);
        Assert.Equal(PlayerOneOutcome, dealResult.PlayerOneOutcome);
        Assert.Equal(PlayerTwoOutcome, dealResult.PlayerTwoOutcome);
        Assert.Equal(PlayerOneSuits, dealResult.PlayerOneSuits);
        Assert.Equal(PlayerTwoSuits, dealResult.PlayerTwoSuits);
    }

    [Fact]
    public void PlayerOneSuits_AllNonSuits_Returns0()
    {
        var dealResult = new DealResult { PlayerOneDeal = "--------------------------" };
        Assert.Equal(0, dealResult.PlayerOneSuits);
    }

    [Fact]
    public void PlayerTwoSuits_AllNonSuits_Returns0()
    {
        var dealResult = new DealResult { PlayerTwoDeal = "--------------------------" };
        Assert.Equal(0, dealResult.PlayerTwoSuits);
    }

    [Fact]
    public void PlayerSuits_AllSuits_Returns26()
    {
        var dealResult = new DealResult 
        { 
            PlayerOneDeal = "AAAAKKKKQQQQJJAAAAKKKKQQ",
            PlayerTwoDeal = "AAAAKKKKQQQQJJAAAAKKKKQQ"
        };
        
        Assert.Equal(26, dealResult.PlayerOneSuits);
        Assert.Equal(26, dealResult.PlayerTwoSuits);
    }
}