namespace BeggarMyNeighbourLibrary;

public static class Engine
{
    private const int FullDeckSize = 52;

    public static DealResult RunScenario(string playerOneCardString, string playerTwoCardString)
    {
        var playerOneCards = new Queue<Card>(
            playerOneCardString.Select(c => new Card(c))
        );

        var playerTwoCards = new Queue<Card>(
            playerTwoCardString.Select(c => new Card(c))
        );

        var dealStatistics = RunScenario(playerOneCards, playerTwoCards);

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

    public static DealStatistics RunScenario(Queue<Card> playerOneCards, Queue<Card> playerTwoCards)
    {
        var playerTwo = false;

        var penalty = 0;
        var tricks = 0;
        var cards = 0;
        var pile = new Queue<Card>(FullDeckSize);

        while (playerOneCards.Count > 0 && playerTwoCards.Count > 0)
        {
            //var card = playerTwo ? playerTwoCards[0] : playerOneCards[0];
            var card = playerTwo ? playerTwoCards.Peek() : playerOneCards.Peek();
            pile.Enqueue(card);

            if (playerTwo)
            {
                playerTwoCards.Dequeue();
            }
            else
            {
                playerOneCards.Dequeue();
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
                            foreach (var p in pile)
                            {
                                playerOneCards.Enqueue(p);
                            }
                        }
                        else
                        {
                            foreach (var p in pile)
                            {
                                playerTwoCards.Enqueue(p);
                            }
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
            PlayerOneOutcome = playerOneCards.Count + pile.Count == FullDeckSize ? "Win" : "Lose",
            PlayerTwoOutcome = playerTwoCards.Count + pile.Count == FullDeckSize ? "Win" : "Lose",
        };

        return dealStatistics;
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