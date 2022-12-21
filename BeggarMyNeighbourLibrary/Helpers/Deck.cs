using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BeggarMyNeighbourLibrary.Helpers
{
    public static class Deck
    {
        public static List<Card> Generate()
        {
            List<Card> retVal = new List<Card>();
            for (int r = 0; r < 13; r++)
            {
                for (int n = 0; n < 4; n++)
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

        public static void GenerateStacks(out List<Card> playerOneStack, out List<Card> playerTwoStack, out string playerOneDeal, out string playerTwoDeal)
        {
            playerOneStack = new List<Card>();
            playerTwoStack = new List<Card>();
            playerOneDeal = string.Empty;
            playerTwoDeal = string.Empty;

            var deck = Generate().OrderBy(x => x.SortOrder);

            playerOneStack = deck.Take(26).ToList();
            playerTwoStack = deck.Skip(26).Take(26).ToList();

            foreach (var c in playerOneStack)
            {
                playerOneDeal += c.SimpleRank.ToString();
            }

            foreach (var c in playerTwoStack)
            {
                playerTwoDeal += c.SimpleRank.ToString();
            }


        }
    }
}
