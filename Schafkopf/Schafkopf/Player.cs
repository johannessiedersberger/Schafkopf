using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schafkopf
{
    public class Player
    {
        private List<Card> _cards;

        public Player(List<Card> cards)
        {
            _cards = cards;
        }
    }
}
