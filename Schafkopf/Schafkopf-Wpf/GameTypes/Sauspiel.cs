using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schafkopf
{
  public class Sauspiel
  {
    public Spieler Spielmacher { get; private set; }
    public Spieler SpielerPartner { get; private set; }
    public SchafkopfSpiel Spiel { get; private set; }
    public Karte GesuchtesAss { get; private set; }

    public Sauspiel(SchafkopfSpiel spiel, Spieler spielmacher, Karte ass)
    {
      if (ass.ToString().Contains('A') == false)
        throw new ArgumentException("Die Karte muss ein Ass sein");
      if (HatSpielerFarbe(spielmacher, ass) == false)
        throw new InvalidOperationException("Der Spieler muss die Farbe haben, nach der er sucht");
      if (spielmacher == SucheSpielerPartner(spiel, ass))
        throw new InvalidOperationException("Der Spieler hat das Ass selber, auf das er spielen will");
      

      Spiel = spiel;
      Spielmacher = spielmacher;
      SpielerPartner = SucheSpielerPartner(spiel, ass);
      GesuchtesAss = ass;
    }

    private static bool HatSpielerFarbe(Spieler spielmacher, Karte ass)
    {
      return spielmacher.Karten.Where(c => c.GetFarbe() == ass.GetFarbe()).Count() > 0;
    }

    private static Spieler SucheSpielerPartner(SchafkopfSpiel game, Karte ass)
    {
      var result = game.SpielerListe.Where(player => player.Karten.Contains(ass));

      if (result.Count() != 0)
        return result.ToList().First();
      else
        throw new InvalidOperationException("No Partner Found");
    }

    public void KartenVergleich(Dictionary<Spieler, Karte>[] playerCards)
    {
      foreach()
    }

    
  }
}
