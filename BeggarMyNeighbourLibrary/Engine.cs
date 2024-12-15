namespace BeggarMyNeighbourLibrary;

public static class Engine
{
    public static async Task<DealResult> RunScenario(string playerOneCardString, string playerTwoCardString)
    {
        var playerOneCards = new List<Card>(26);
        var playerTwoCards = new List<Card>(26);

        foreach (var c in playerOneCardString)
        {
            playerOneCards.Add(new Card(c));
        }

        foreach (var c in playerTwoCardString)
        {
            playerTwoCards.Add(new Card(c));
        }

        var dealStatistics = await RunScenario(playerOneCards, playerTwoCards);

        return new DealResult
        {
            PlayerOneDeal = playerOneCardString,
            PlayerTwoDeal = playerTwoCardString,
            Cards = dealStatistics.Cards,
            Tricks = dealStatistics.Tricks
        };
    }

    public static async Task<DealStatistics> RunScenario(List<Card> playerOneCards, List<Card> playerTwoCards)
    {
        return await Task.Run(() =>
        {
            var dealStatistics = new DealStatistics();
            var playerTwo = false;

            var penalty = 0;
            var tricks = 0;
            var cards = 0;
            var pile = new List<Card>(52);

            while (playerOneCards.Count > 0 && playerTwoCards.Count > 0)
            {
                var card = playerTwo ? playerTwoCards[0] : playerOneCards[0];
                pile.Add(card);

                if (playerTwo)
                    playerTwoCards.RemoveAt(0);
                else
                    playerOneCards.RemoveAt(0);

                cards++;

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

                playerTwo = !playerTwo;
            }

            if (pile.Count > 0)
            {
                tricks++;
            }

            dealStatistics.Cards = cards;
            dealStatistics.Tricks = tricks;

            return dealStatistics;
        });
    }
}