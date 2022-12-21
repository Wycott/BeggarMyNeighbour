using BeggarMyNeighbourLibrary;
using BeggarMyNeighbourLibrary.Helpers;
using static System.Console;

namespace BeggarMyNeighbour;

internal class Program
{
    private static void Main(string[] args)
    {
        var maxCards = 0;
        var iterations = 0;
        var record = 0;

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

            WriteLine($"{record}) {DateTime.Now}");
            WriteLine($"Most cards played is {maxCards} after {iterations} iterations");
            WriteLine($"Most tricks played is {tricks} after {iterations} iterations");
            WriteLine(playerOneDeal);
            WriteLine(playerTwoDeal);
            WriteLine();
        }
    }

    private static void NormalPlay(out int cards, out int tricks, out string playerOneDeal, out string playerTwoDeal)
    {
        Deck.GenerateStacks(out var playerOneStack, out var playerTwoStack, out playerOneDeal, out playerTwoDeal);
        Engine.RunScenario(playerOneStack, playerTwoStack, out cards, out tricks);
    }
}