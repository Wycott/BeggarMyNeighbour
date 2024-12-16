﻿using System.Collections.Concurrent;

namespace BeggarMyNeighbourLibrary.Helpers;

public static class Deck
{
    public static async Task<Deal> GenerateStacks()
    {
        var deckOfCards = await Generate();

        return await CreateDealFromDeck(deckOfCards);
    }

    public static async Task<Deal> CreateDealFromDeck(List<Card> deckOfCards)
    {
        return await Task.Run(() =>
        {
            var playerOneCards = deckOfCards.OrderBy(x => x.SortOrder).Take(26).ToList();
            var playerTwoCards = deckOfCards.OrderBy(x => x.SortOrder).Skip(26).Take(26).ToList();

            var playerOneDealBag = new ConcurrentBag<string>();
            var playerTwoDealBag = new ConcurrentBag<string>();

            Parallel.ForEach(playerOneCards, c =>
            {
                playerOneDealBag.Add(c.SimpleRank.ToString());
            });

            Parallel.ForEach(playerTwoCards, c =>
            {
                playerTwoDealBag.Add(c.SimpleRank.ToString());
            });

            var playerOneDeal = string.Join("", playerOneDealBag);
            var playerTwoDeal = string.Join("", playerTwoDealBag);

            return new Deal
            {
                PlayerOneCards = playerOneCards,
                PlayerTwoCards = playerTwoCards,
                PlayerOneDeal = playerOneDeal,
                PlayerTwoDeal = playerTwoDeal
            };
        });
    }

    //public static async Task<Deal> CreateDealFromDeck(List<Card> deckOfCards)
    //{
    //    return await Task.Run(() =>
    //    {
    //        var playerOneDeal = string.Empty;
    //        var playerTwoDeal = string.Empty;
    //        var playerOneCards = deckOfCards.OrderBy(x => x.SortOrder).Take(26).ToList();
    //        var playerTwoCards = deckOfCards.OrderBy(x => x.SortOrder).Skip(26).Take(26).ToList();

    //        foreach (var c in playerOneCards)
    //        {
    //            playerOneDeal += c.SimpleRank.ToString();
    //        }

    //        foreach (var c in playerTwoCards)
    //        {
    //            playerTwoDeal += c.SimpleRank.ToString();
    //        }

    //        return new Deal
    //        {
    //            PlayerOneCards = playerOneCards,
    //            PlayerTwoCards = playerTwoCards,
    //            PlayerOneDeal = playerOneDeal,
    //            PlayerTwoDeal = playerTwoDeal
    //        };
    //    });
    //}

    public static async Task<List<Card>> Generate()
    {
        return await Task.Run(() =>
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
        });
    }
}