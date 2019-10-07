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
      if (ass.SchlagWert != Schlag.Ass)
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
      return spielmacher.Karten.Where(c => c.FarbenWert == ass.FarbenWert).Count() > 0;
    }

    private static Spieler SucheSpielerPartner(SchafkopfSpiel game, Karte ass)
    {
      var result = game.SpielerListe.Where(player => player.Karten.Contains(ass));

      if (result.Count() != 0)
        return result.ToList().First();
      else
        throw new InvalidOperationException("No Partner Found");
    }

    public Karte KartenVergleich(Karte[] karten, Karte ersteKarte)
    {
      Karte tempKarte = null;

      for (int write = 0; write < karten.Length; write++)
      {
        for (int sort = 0; sort < karten.Length - 1; sort++)
        {
          if(karten[sort] != HoechsteKarte(karten[sort], karten[sort+1], ersteKarte))
          {
            tempKarte = karten[sort + 1];
            karten[sort + 1] = karten[sort];
            karten[sort] = tempKarte;
          }
        }
      }

      return karten[0]; // hoechste Karte
    }

    internal Karte HoechsteKarte(Karte karte1, Karte karte2, Karte ersteGespielteKarte) // 2 Karten
    {
      Karte[] karten = new Karte[] { karte1 , karte2 };
      var ober = GetOber(karten);
      var unter = GetUnter(karten);
      var herzen = GetHerzen(karten);

      if (ober.Length > 0) // Ober
      {
        if (ober.Length == 2)
          return HoechsteFarbe(karten);
        else // nur 1 Ober
          return ober[0];
      }
      else if(unter.Length> 0) // Unter
      {
        if (unter.Length == 2)
          return HoechsteFarbe(karten);
        else // nur 1 Unter
          return unter[0];
      }
      else if(herzen.Length > 0) // Herz
      {       
        if (herzen.Count() == 2)
          return HoechstePunkte(karten);
        else // Herzen == 1
          return herzen.First();
      }
      else // Andere Farben
      {
        if (karten[0].FarbenWert == karten[1].FarbenWert) // Gleiche Farbe
        {
          return HoechstePunkte(karten);
        }
        else // Ungleiche Farbe
        {
          var ersteKarteFarben = karten.Where(karte => karte.FarbenWert == ersteGespielteKarte.FarbenWert);

          if(ersteKarteFarben.Count() == 1)
            return ersteKarteFarben.First();

          return karten[0];
        }
          
      }
      
    }

    private static Karte[] GetOber(Karte[] karten)
    {
      return karten.Where(karte => karte.SchlagWert == Schlag.Ober).ToArray();
    }

    private static Karte[] GetUnter(Karte[] karten)
    {
      return karten.Where(karte => karte.SchlagWert == Schlag.Unter).ToArray();
    }

    private static Karte[] GetHerzen(Karte[] karten)
    {
      return karten.Where(karte => karte.FarbenWert == Farbe.Herz).ToArray();
    }

    internal Karte HoechsteFarbe(Karte[] karten)
    {
      if (karten.Length != 2)
        throw new ArgumentException("only 2 cards accepted");

      if (karten[0].FarbenWert == karten[1].FarbenWert)
        throw new ArgumentException("Gleiche Farben");

      Farbe[] farben = Extensions.AlleFarben();
      foreach(Farbe f in farben)
      {
        var result = karten.Where(karte => karte.FarbenWert == f);
        if (result.Count() != 0)
          return result.First();
      }
      throw new InvalidOperationException("HoechsteFarbe not Found");
    }

    internal Karte HoechstePunkte(Karte[] karten)
    {
      if (karten[0].SchlagWert == karten[1].SchlagWert)
        throw new ArgumentException("Gleiche Farben");

      if (karten.Length != 2)
        throw new ArgumentException("only 2 cards accepted");

      Schlag[] schlaege = Extensions.AlleSchlaege();

      foreach (Schlag schlag in schlaege)
      {
        var result = karten.Where(karte => karte.SchlagWert == schlag);
        if (result.Count() != 0)
          return result.First();
      }
      throw new InvalidOperationException("Hoechste Punkte not Found");
    }

    
  }
}
