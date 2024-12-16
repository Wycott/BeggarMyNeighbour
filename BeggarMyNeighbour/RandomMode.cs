using BeggarMyNeighbourLibrary.Helpers;
using BeggarMyNeighbourLibrary;
using System.Diagnostics;

namespace BeggarMyNeighbour
{
    internal partial class Program
    {
        private static async Task RandomMode()
        {
            Stopwatch sw = new Stopwatch();
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

        private static async Task<DealResult> RandomPlay()
        {
            var deal = await Deck.GenerateStacks();

            return await Engine.RunScenario(deal.PlayerOneDeal, deal.PlayerTwoDeal);
        }
    }
}
