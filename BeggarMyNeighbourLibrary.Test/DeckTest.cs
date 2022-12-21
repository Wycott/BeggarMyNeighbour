using BeggarMyNeighbourLibrary.Helpers;

namespace BeggarMyNeighbourLibrary.Test
{
    public class DeckTest
    {
        [Fact]
        public void Test1()
        {
            var res = Deck.Generate();

            Assert.Equal(52, res.Count);
        }

        [Fact]
        public void Test2()
        {
            const int ExpectedLength = 26;
            Deck.GenerateStacks(out var playerOneStack, out var playerTwoStack, out var playerOneDeal, out var playerTwoDeal);

            Assert.Equal(ExpectedLength, playerOneStack.Count);
            Assert.Equal(ExpectedLength, playerTwoStack.Count);
            Assert.Equal(ExpectedLength, playerOneDeal.Length);
            Assert.Equal(ExpectedLength, playerTwoDeal.Length);
        }
    }
}