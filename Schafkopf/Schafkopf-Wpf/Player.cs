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

    public List<Card> Cards
    {
      get
      {
        var copy = new Card[8];
        _cards.CopyTo(copy);
        return copy.ToList();
      }
    }

    public Player(List<Card> cards)
    {
      _cards = cards;
    }
  }
}
