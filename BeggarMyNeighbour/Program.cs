using BeggarMyNeighbourLibrary;
using BeggarMyNeighbourLibrary.Helpers;

namespace BeggarMyNeighbour
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int maxCards = 0;
            //int maxTricks = 0;
            int iterations = 0;
            int record = 0;
            while (true)
            {
                iterations++;
                NormalPlay(out var cards, out var tricks, out var playerOneDeal, out var playerTwoDeal);
                //bool spaceIt = false;
                if (cards > maxCards)
                {
                    record++;
                    maxCards = cards;
                    Console.WriteLine($"{record}) {DateTime.Now}");
                    Console.WriteLine($"Most cards played is {maxCards} after {iterations} iterations");
                    //aceIt = true;

                    //if (tricks > maxTricks)
                    //{
                    //maxTricks = tricks;
                    Console.WriteLine($"Most tricks played is {tricks} after {iterations} iterations");
                    //  spaceIt = true;
                    //}
                    Console.WriteLine(playerOneDeal);
                    Console.WriteLine(playerTwoDeal);
                    Console.WriteLine();
                }



//                if (spaceIt)
  //              {

    //            }

            }
            //ScenarioPlay1();
            //ScenarioPlay2();
        }

        static void NormalPlay(out int cards, out int tricks, out string playerOneDeal, out string playerTwoDeal)
        {
            Deck.GenerateStacks(out var playerOneStack, out var playerTwoStack, out playerOneDeal, out playerTwoDeal);
            Engine.RunScenario(playerOneStack, playerTwoStack, out cards, out tricks);
        }

        //static void ScenarioPlay1()
        //{
        //    Engine.RunScenario(
        //        "---AJ--Q---------QAKQJJ-QK",
        //        "-----A----KJ-K--------A---", out var cards, out var tricks);
        //}

        //static void ScenarioPlay2()
        //{
        //    Engine.RunScenario(
        //        "----K---A--Q-A--JJA------J",
        //        "-----KK---------A-JK-Q-Q-Q", out var cards, out var tricks);
        //}
    }
}