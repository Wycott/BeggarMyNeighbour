using System.Globalization;
using BeggarMyNeighbourLibrary;
using BeggarMyNeighbourLibrary.Helpers;


namespace BeggarMyNeighbour;

internal class Program
{
    private static void Main(string[] args)
    {
        var maxCards = 0;
        var iterations = 0;
        var record = 0;

        var fileNameStub = Guid.NewGuid().ToString();

        while (true)
        {
            iterations++;
            NormalPlay(out var cards, out var tricks, out var playerOneDeal, out var playerTwoDeal);

            if (cards <= maxCards)
            {
                continue;
            }

            record++;
            maxCards = cards;

            var resultLine1 = $"{record}) {DateTime.Now}";
            var resultLine2 = $"Most cards played is {maxCards} after {iterations} iterations";
            var resultLine3 = $"Most tricks played is {tricks} after {iterations} iterations";
            var resultLine4 = playerOneDeal;
            var resultLine5 = playerTwoDeal;
            var resultLine6 = string.Empty;

            Output.WriteResults(fileNameStub, resultLine1, resultLine2, resultLine3, resultLine4, resultLine5, resultLine6);
        }
    }

    private static void NormalPlay(out int cards, out int tricks, out string playerOneDeal, out string playerTwoDeal)
    {
        Deck.GenerateStacks(out var playerOneStack, out var playerTwoStack, out playerOneDeal, out playerTwoDeal);
        Engine.RunScenario(playerOneStack, playerTwoStack, out cards, out tricks);
    }

    private static void ScenarioPlay1()
    {
        Engine.RunScenario(
            "---AJ--Q---------QAKQJJ-QK",
            "-----A----KJ-K--------A---",
            out var cards,
            out var tricks);
    }

    private static void ScenarioPlay2()
    {
        Engine.RunScenario(
            "----K---A--Q-A--JJA------J",
            "-----KK---------A-JK-Q-Q-Q",
            out var cards,
            out var tricks);
    }
}