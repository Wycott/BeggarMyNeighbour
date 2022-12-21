using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeggarMyNeighbourLibrary
{
    public static class Engine
    {
        public static void RunScenario(string playerOneCardString, string playerTwoCardString, out int cards, out int tricks)
        {
            List<Card> playerOneCards = new List<Card>();
            List<Card> playerTwoCards = new List<Card>();

            foreach (char c in playerOneCardString)
            {
                playerOneCards.Add(new Card(c));
            }

            foreach (char c in playerTwoCardString)
            {
                playerTwoCards.Add(new Card(c));
            }

            RunScenario(playerOneCards, playerTwoCards, out cards, out tricks);
        }

        public static void RunScenario(List<Card> playerOneCards, List<Card> playerTwoCards, out int cards, out int tricks)
        {
            bool playerTwo = false;

            int penalty = 0;
            tricks = 0;
            cards = 0;
            List<Card> pile = new List<Card>();

            while (playerOneCards.Count > 0 && playerTwoCards.Count > 0)
            {
                var card = playerTwo ? playerTwoCards[0] : playerOneCards[0];
                pile.Add(card);

                //if (playerTwo)
                //    Console.WriteLine($"Player 2 plays: {card.SimpleRank.ToString()}");
                //else
                //    Console.WriteLine($"Player 1 plays: {card.SimpleRank.ToString()}");

                if (playerTwo)
                    playerTwoCards.RemoveAt(0);
                else
                    playerOneCards.RemoveAt(0);

                cards++;

                //bool penaltyInPlay = false;

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
                                //Console.WriteLine($"Player 1 wins and has : {playerOneCards.Count}");
                                //Console.WriteLine($"Player 2 has          : {playerTwoCards.Count}");
                            }
                            else
                            {
                                playerTwoCards.AddRange(pile);
                                //Console.WriteLine($"Player 2 wins and has : {playerTwoCards.Count}");
                                //Console.WriteLine($"Player 1 has          : {playerOneCards.Count}");
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
                tricks++;
        }
    }
}
