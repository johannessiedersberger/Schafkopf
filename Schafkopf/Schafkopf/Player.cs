using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schafkopf
{
    public class Player
    {
        public List<Card> Cards { get; private set; }

        public Player(List<Card> cards)
        {
            Cards = cards;
        }
    }
}
