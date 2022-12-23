namespace BeggarMyNeighbourLibrary;

public static class Engine
{
    public static void RunScenario(string playerOneCardString, string playerTwoCardString, out int cards, out int tricks)
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

        RunScenario(playerOneCards, playerTwoCards, out cards, out tricks);
    }

    public static void RunScenario(List<Card> playerOneCards, List<Card> playerTwoCards, out int cards, out int tricks)
    {
        var playerTwo = false;

        var penalty = 0;
        tricks = 0;
        cards = 0;
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
    }
}