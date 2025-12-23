using System.Collections.Concurrent;

namespace BeggarMyNeighbourLibrary.Helpers;

public static class Deck
{
    public static Deal GenerateStacks()
    {
        var deckOfCards = Generate();

        return CreateDealFromDeck(deckOfCards);
    }

    public static Deal CreateDealFromDeck(List<Card> deckOfCards)
    {
        // Fisher-Yates shuffle
        var n = deckOfCards.Count;
        while (n > 1)
        {
            n--;
            var k = Random.Shared.Next(n + 1);
            (deckOfCards[k], deckOfCards[n]) = (deckOfCards[n], deckOfCards[k]);
        }

        var playerOneCards = deckOfCards.Take(26).ToList();
        var playerTwoCards = deckOfCards.Skip(26).Take(26).ToList();

        var sb1 = new System.Text.StringBuilder(26);
        foreach (var c in playerOneCards) sb1.Append(c.SimpleRank);

        var sb2 = new System.Text.StringBuilder(26);
        foreach (var c in playerTwoCards) sb2.Append(c.SimpleRank);

        return new Deal
        {
            PlayerOneCards = playerOneCards,
            PlayerTwoCards = playerTwoCards,
            PlayerOneDeal = sb1.ToString(),
            PlayerTwoDeal = sb2.ToString()
        };
    }

    public static List<Card> Generate()
    {
        var retVal = new List<Card>(52);

        for (var r = 0; r < 13; r++)
        {
            for (var n = 0; n < 4; n++)
            {
                switch (r)
                {
                    case 0:
                        retVal.Add(new Card('A'));
                        break;
                    case 1:
                        retVal.Add(new Card('K'));
                        break;
                    case 2:
                        retVal.Add(new Card('Q'));
                        break;
                    case 3:
                        retVal.Add(new Card('J'));
                        break;
                    default:
                        retVal.Add(new Card('-'));
                        break;
                }
            }
        }

        return retVal;

    }
}