using BeggarMyNeighbourLibrary;

namespace BeggarMyNeighbour
{
    internal partial class Program
    {
        // ReSharper disable once UnusedMember.Local
        private static async Task<DealResult> ScenarioPlay1()
        {
            return await Engine.RunScenario(
                "---AJ--Q---------QAKQJJ-QK",
                "-----A----KJ-K--------A---");
        }

        // ReSharper disable once UnusedMember.Local
        private static async Task<DealResult> ScenarioPlay2()
        {
            return await Engine.RunScenario(
                "----K---A--Q-A--JJA------J",
                "-----KK---------A-JK-Q-Q-Q");
        }
    }
}
