using BeggarMyNeighbourLibrary;
using BeggarMyNeighbourLibrary.Helpers;

namespace BeggarMyNeighbour;

internal class Program
{
    private static async Task Main()
    {
        var maxCards = 0;
        var iterations = 0;
        var record = 0;

        var fileNameStub = Guid.NewGuid().ToString();

        while (true)
        {
            iterations++;
            var dealResult = await NormalPlay();

            if (dealResult.Cards <= maxCards)
            {
                continue;
            }

            record++;
            maxCards = dealResult.Cards;
            var maxDecks = dealResult.Cards / 52;

            var resultLine1 = $"{record}) {DateTime.Now}";
            var resultLine2 = $"After {iterations} iterations";
            var resultLine3 = $"Most cards played is {maxCards}";
            var resultLine4 = $"Most decks played is {maxDecks}";
            var resultLine5 = $"Most tricks played is {dealResult.Tricks}";
            var resultLine8 = string.Empty;

            Output.WriteResults(fileNameStub,
                resultLine1,
                                resultLine2,
                                resultLine3,
                                resultLine4,
                                resultLine5,
                                dealResult.PlayerOneDeal,
                                dealResult.PlayerTwoDeal,
                                resultLine8);
        }
        // ReSharper disable once FunctionNeverReturns
    }

    private static async Task<DealResult> NormalPlay()
    {
        
        var deal = await Deck.GenerateStacks();
        return await Engine.RunScenario(deal.PlayerOneDeal, deal.PlayerTwoDeal);
    }

    private static async Task<DealResult> ScenarioPlay1()
    {
        return await Engine.RunScenario(
            "---AJ--Q---------QAKQJJ-QK",
            "-----A----KJ-K--------A---");
    }

    private static async Task<DealResult> ScenarioPlay2()
    {
        return await Engine.RunScenario(
            "----K---A--Q-A--JJA------J",
            "-----KK---------A-JK-Q-Q-Q");
    }
}