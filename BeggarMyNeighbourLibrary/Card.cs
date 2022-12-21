using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeggarMyNeighbourLibrary
{
    public  class Card
    {
        public Card(char simpleRank)
        {
            SimpleRank = simpleRank;
            SortOrder = Guid.NewGuid().ToString();
        }

        public char SimpleRank { get; set; }
        public string SortOrder { get; set; }
    }
}
