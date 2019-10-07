using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schafkopf
{
  public class Spieler
  {
    public List<Karte[]> Stiche { get; }
    

    public List<Karte> Karten { get; }
   

    public Spieler(List<Karte> karten)
    {
      Karten = karten;
      SetzeEigentuemer(karten, this);
    }

    private static void SetzeEigentuemer(List<Karte> karten, Spieler spieler)
    {
      foreach(var karte in karten)
      {
        karte.Eigentuemer = spieler;
      }
    }
  }
}
