namespace BeggarMyNeighbourLibrary.Test;

public class DealResultTest
{
    [Fact]
    public void GenerateStacks_CreatesTwoStacksOfCards()
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
}