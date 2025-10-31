using BeggarMyNeighbourLibrary.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeggarMyNeighbourLibrary.Test
{
    public class EngineTest
    {
        [Fact]
        public async void Scenario1()
        {
            const int ExpectedTricks = 1164;
            const int ExpectedCards = 8344;

            var res = await Engine.RunScenario(
                "---AJ--Q---------QAKQJJ-QK",
                "-----A----KJ-K--------A---");

            Assert.Equal(ExpectedTricks, res.Tricks);
            Assert.Equal(ExpectedCards, res.Cards);
        }

        [Fact]
        public async void Scenario2()
        {
            const int ExpectedTricks = 1106;
            const int ExpectedCards = 7972;
            
            var res = await Engine.RunScenario(
                "----K---A--Q-A--JJA------J",
                "-----KK---------A-JK-Q-Q-Q");

            Assert.Equal(ExpectedTricks, res.Tricks);
            Assert.Equal(ExpectedCards, res.Cards);
        }
    }
}
