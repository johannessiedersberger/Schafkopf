using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schafkopf
{
  /// <summary>
  /// The Game
  /// </summary>
  public class SchafkopfSpiel
  {
    /// <summary>
    /// Contains the Players
    /// </summary>
    public List<Spieler> SpielerListe { get; private set; }

    /// <summary>
    /// Creates the Players and distributes the cards
    /// </summary>
    public SchafkopfSpiel()
    {
      var cards = ErstelleAlleKarten();
      SpielerListe = ErstelleSpielerListe(cards);
    }

    /// <summary>
    /// Create game with predifined Players
    /// for Test Purposes
    /// </summary>
    /// <param name="spieler"></param>
    internal SchafkopfSpiel(List<Spieler> spieler)
    {
      SpielerListe = spieler;
    }

    #region CardDistribution
    private static List<Kartenwerte> ErstelleAlleKarten()
    {
      return Enum.GetValues(typeof(Kartenwerte)).Cast<Kartenwerte>().ToList();
    }

    private static List<Spieler> ErstelleSpielerListe(List<Kartenwerte> kartenWerte)
    {
      var spieler = new List<Spieler>();
      for (int i = 0; i < 4; i++)
      {
        spieler.Add(new Spieler(VerteileKarten(kartenWerte)));
      }
      return spieler;
    }

    private static List<Karte> VerteileKarten(List<Kartenwerte> kartenWerte)
    {
      List<Karte> kartenFuerSpieler = new List<Karte>();
      for (int i = 0; i < 8; i++)
      {
        GibKarte(kartenWerte, kartenFuerSpieler);
      }
      return kartenFuerSpieler.ToList();
    }

    private static void GibKarte(List<Kartenwerte> kartenWerte, List<Karte> cardsForPlayer)
    {
      int cardIndex = ZufaelligeKartenNummer(kartenWerte);
      var kartenWert = kartenWerte[cardIndex];
      kartenWerte.Remove(kartenWert);
      cardsForPlayer.Add(new Karte(kartenWert));
    }

    private static int ZufaelligeKartenNummer(List<Kartenwerte> cards)
    {
      return new Random(System.DateTime.Now.Millisecond.GetHashCode()).Next(0, cards.Count() - 1);
    }

    #endregion

    


  }
}
