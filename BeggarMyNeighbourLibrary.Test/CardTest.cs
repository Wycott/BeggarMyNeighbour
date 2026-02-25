using AiAnnotations;
using AiAnnotations.Types;

namespace BeggarMyNeighbourLibrary.Test;

[AiGenerated]
public class CardTest
{
    [Fact]
    public void Constructor_SetsSimpleRank()
    {
        var card = new Card('A');
        
        Assert.Equal('A', card.SimpleRank);
    }

    [Fact]
    public void SimpleRank_CanBeModified()
    {
        var card = new Card('K') { SimpleRank = 'Q' };
        
        Assert.Equal('Q', card.SimpleRank);
    }

    [Theory]
    [InlineData('A')]
    [InlineData('K')]
    [InlineData('Q')]
    [InlineData('J')]
    [InlineData('-')]
    public void Card_SupportsAllRanks(char rank)
    {
        var card = new Card(rank);
        
        Assert.Equal(rank, card.SimpleRank);
    }
}
