using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schafkopf
{
  public class Player
  {
    public List<Card[]> Stiche { get; }
    
    public List<Card> Cards { get; }
   
    public Player(List<Card> karten)
    {
      Cards = karten;
      SetOwner(karten, this);
    }

    private static void SetOwner(List<Card> karten, Player spieler)
    {
      foreach(var karte in karten)
      {
        karte.Owner = spieler;
      }
    }
  }
}
