using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schafkopf
{
  public class Spieler
  {
    public List<Karte> Stiche
    {
      get
      {
        var copy = new Karte[8];
        _stichCards.CopyTo(copy);
        return copy.ToList();
      }
    }
    private List<Karte> _stichCards;

    public List<Karte> Karten
    {
      get
      {
        var copy = new Karte[8];
        _cards.CopyTo(copy);
        return copy.ToList();
      }
    }
    private List<Karte> _cards;

    public Spieler(List<Karte> karten)
    {
      _cards = karten;
    }
  }
}
