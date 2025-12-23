using BeggarMyNeighbourLibrary.Helpers;
using BeggarMyNeighbourLibrary;
using System.Diagnostics;

namespace BeggarMyNeighbour;

internal partial class Program
{
    private static long bestRate = 0;

    private static readonly Dictionary<string, string> ReleaseNotes = new()
    {
        {"Damp squib", "14 November 2025 - Initial cut"},
        {"Night goose", "23 December 2025 - i) Change Iters calculation to long as it overran"},
        {"Half bee", "23 December 2025 - ii) Antigravity tuning"},
        {"Wooden sloggie", "23 December 2025 - iii) More antigravity tuning"},
    };

    private static void RandomMode()
    {
        var sw = new Stopwatch();
        sw.Start();

        var maxCards = 0;
        var maxTricks = 0;
        var iterations = 0L;
        var record = 0;

        var fileNameStub = Guid.NewGuid().ToString();

        while (true)
        {
            iterations++;
            var dealResult = RandomPlay();

            var newBest = false;

            if (dealResult.Cards > maxCards)
            {
                maxCards = dealResult.Cards;
                newBest = true;
            }

            if (dealResult.Tricks > maxTricks)
            {
                maxTricks = dealResult.Tricks;
                newBest = true;
            }

            if (newBest)
            {
                OutputBestResultSoFar(dealResult, iterations, fileNameStub, ref record, sw);
            }
        }
        // ReSharper disable once FunctionNeverReturns
    }

    private static void OutputBestResultSoFar(DealResult dealResult, long iterations, string fileNameStub, ref int record, Stopwatch sw)
    {
        var releaseCodeName = ReleaseNotes.Keys.Last();

        record++;

        var maxCards = dealResult.Cards;
        var maxDecks = dealResult.Cards / 52;
        var secs = Math.Max(sw.ElapsedMilliseconds / 1000, 1);
        var rate = iterations / secs;

        if (rate > bestRate)
        {
            bestRate = rate;
        }

        var resultLine0 = sw.ElapsedMilliseconds >= 60000
            ? $"Runtime {sw.ElapsedMilliseconds / 60000:N0} mins"
            : $"Runtime {secs} secs";
        var resultLine1 = $"{record}) {DateTime.Now}";
        var resultLine2 = $"Iters {iterations:N0} ({rate:N0}/s)";
        var resultLine2b = $"Best rate {bestRate:N0}";
        var resultLine3 = $"Cards played {maxCards:N0}";
        var resultLine4 = $"Decks played {maxDecks:N0}";
        var resultLine5 = $"Tricks played {dealResult.Tricks:N0}";
        var resultLine6 = releaseCodeName;
        var resultLine8 = string.Empty;

        Output.WriteResults(fileNameStub,
            resultLine1,
            resultLine0,
            resultLine2,
            resultLine2b,
            resultLine3,
            resultLine4,
            resultLine5,
            dealResult.PlayerOneDeal + $" {dealResult.PlayerOneSuits} " + dealResult.PlayerOneOutcome,
            dealResult.PlayerTwoDeal + $" {dealResult.PlayerTwoSuits} " + dealResult.PlayerTwoOutcome,
            resultLine6,
            resultLine8);
    }

    private static DealResult RandomPlay()
    {
        var deal = Deck.GenerateStacks();

        return Engine.RunScenario(deal.PlayerOneDeal, deal.PlayerTwoDeal);
    }
}