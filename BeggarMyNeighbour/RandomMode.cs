using BeggarMyNeighbourLibrary.Helpers;
using BeggarMyNeighbourLibrary;
using System.Diagnostics;

namespace BeggarMyNeighbour;

internal partial class Program
{
    private static async Task RandomMode()
    {
        var sw = new Stopwatch();
        sw.Start();

        var maxCards = 0;
        var iterations = 0;
        var record = 0;

        var fileNameStub = Guid.NewGuid().ToString();

        while (true)
        {
            iterations++;
            var dealResult = await RandomPlay();

            if (dealResult.Cards <= maxCards)
            {
                continue;
            }

            maxCards = OutputBestResultSoFar(dealResult, iterations, fileNameStub, ref record, sw);
        }
        // ReSharper disable once FunctionNeverReturns
    }

    private static int OutputBestResultSoFar(DealResult dealResult, int iterations, string fileNameStub, ref int record, Stopwatch sw)
    {
        record++;

        var maxCards = dealResult.Cards;
        var maxDecks = dealResult.Cards / 52;

        var resultLine0 = sw.ElapsedMilliseconds >= 60000
            ? $"Runtime {sw.ElapsedMilliseconds / 60000:N0} mins"
            : $"Runtime {sw.ElapsedMilliseconds / 1000} secs";
        var resultLine1 = $"{record}) {DateTime.Now}";
        var resultLine2 = $"After {iterations:N0} iterations";
        var resultLine3 = $"Most cards played is {maxCards:N0}";
        var resultLine4 = $"Most decks played is {maxDecks:N0}";
        var resultLine5 = $"Most tricks played is {dealResult.Tricks:N0}";
        var resultLine8 = string.Empty;

        Output.WriteResults(fileNameStub,
            resultLine1,
            resultLine0,
            resultLine2,
            resultLine3,
            resultLine4,
            resultLine5,
            dealResult.PlayerOneDeal + $" {dealResult.PlayerOneSuits} " + dealResult.PlayerOneOutcome,
            dealResult.PlayerTwoDeal + $" {dealResult.PlayerTwoSuits} " + dealResult.PlayerTwoOutcome,
            resultLine8);

        return maxCards;
    }

    private static async Task<DealResult> RandomPlay()
    {
        var deal = await Deck.GenerateStacks();

        return await Engine.RunScenario(deal.PlayerOneDeal, deal.PlayerTwoDeal);
    }
}