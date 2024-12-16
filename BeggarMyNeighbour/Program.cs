using System.Diagnostics;
using BeggarMyNeighbourLibrary;
using BeggarMyNeighbourLibrary.Helpers;

namespace BeggarMyNeighbour;

internal partial class Program
{
    private static async Task Main()
    {
        await RandomMode();
    }

    private static int OutputBestResultSoFar(DealResult dealResult, int iterations, string fileNameStub, ref int record, Stopwatch sw)
    {
        record++;
        
        var maxCards = dealResult.Cards;
        var maxDecks = dealResult.Cards / 52;

        var resultLine0 = sw.ElapsedMilliseconds >= 60000
            ? $"Runtime: {sw.ElapsedMilliseconds / 60000} mins"
            : $"Runtime: {sw.ElapsedMilliseconds / 1000} secs";
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
            dealResult.PlayerOneDeal,
            dealResult.PlayerTwoDeal,
            resultLine8);

        return maxCards;
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