using System.Collections;
using System.Collections.Concurrent;

namespace BeggarMyNeighbourLibrary;

public static class Engine
{
    private const int FullDeckSize = 52;

    public static async Task<DealResult> RunScenario(string playerOneCardString, string playerTwoCardString)
    {
        var playerOneCards = new ConcurrentBag<Card>();
        var playerTwoCards = new ConcurrentBag<Card>();

        Parallel.ForEach(playerOneCardString, c =>
        {
            playerOneCards.Add(new Card(c));
        });

        Parallel.ForEach(playerTwoCardString, c =>
        {
            playerTwoCards.Add(new Card(c));

        });

        var dealStatistics = await RunScenario(playerOneCards.ToList(), playerTwoCards.ToList());

        return new DealResult
        {
            PlayerOneDeal = playerOneCardString,
            PlayerTwoDeal = playerTwoCardString,
            Cards = dealStatistics.Cards,
            Tricks = dealStatistics.Tricks,
            PlayerOneOutcome = dealStatistics.PlayerOneOutcome,
            PlayerTwoOutcome = dealStatistics.PlayerTwoOutcome
        };
    }

    public static async Task<DealStatistics> RunScenario(List<Card> playerOneCards, List<Card> playerTwoCards)
    {
        return await Task.Run(() =>
        {
            var playerTwo = false;

            var penalty = 0;
            var tricks = 0;
            var cards = 0;
            var pile = new List<Card>(FullDeckSize);

            while (playerOneCards.Count > 0 && playerTwoCards.Count > 0)
            {
                var card = playerTwo ? playerTwoCards[0] : playerOneCards[0];
                pile.Add(card);

                if (playerTwo)
                {
                    playerTwoCards.RemoveAt(0);
                }
                else
                {
                    playerOneCards.RemoveAt(0);
                }

                cards++;

                // Best known is 8344 as of 16 December 2024.
                if (cards > 20000)
                {
                    Console.WriteLine("Looping. Exiting. ");
                    Environment.Exit(0);
                }

                if (penalty > 0)
                {
                    penalty--;

                    if (card.SimpleRank == '-')
                    {
                        if (penalty == 0)
                        {
                            tricks++;

                            if (playerTwo)
                            {
                                playerOneCards.AddRange(pile);
                            }
                            else
                            {
                                playerTwoCards.AddRange(pile);
                            }

                            pile.Clear();
                            playerTwo = !playerTwo;
                        }
                        continue;
                    }
                }

                penalty = CalculatePenalty(card, penalty);

                playerTwo = !playerTwo;
            }

            if (pile.Count > 0)
            {
                tricks++;
            }

            var dealStatistics = new DealStatistics
            {
                Cards = cards,
                Tricks = tricks,
                PlayerOneOutcome = CalculateOutcome(playerOneCards, pile),
                PlayerTwoOutcome = CalculateOutcome(playerTwoCards, pile),
            };

            return dealStatistics;
        });
    }

    private static string CalculateOutcome(ICollection playerStack, ICollection pileStack)
    {
        return playerStack.Count + pileStack.Count == FullDeckSize ? "Win" : "Lose";
    }

    private static int CalculatePenalty(Card card, int penalty)
    {
        switch (card.SimpleRank)
        {
            case '-':
                break;
            case 'A':
                penalty = 4;
                break;
            case 'K':
                penalty = 3;
                break;
            case 'Q':
                penalty = 2;
                break;
            case 'J':
                penalty = 1;
                break;
        }

        return penalty;
    }
}