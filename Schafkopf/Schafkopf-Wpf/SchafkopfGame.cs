using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schafkopf
{
  /// <summary>
  /// Contains the cards for the Game
  /// </summary>
  public enum Karte
  {
    //7 8 9 10 U O K A
    E7, E8, E9, E10, EU, EO, EK, EA, // Eichel
    G7, G8, G9, G10, GU, GO, GK, GA, // Gras
    H7, H8, H9, H10, HU, HO, HK, HA, // Herz
    S7, S8, S9, S10, SU, SO, SK, SA, // Schellen
  }

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
    private static List<Karte> ErstelleAlleKarten()
    {
      return Enum.GetValues(typeof(Karte)).Cast<Karte>().ToList();
    }

    private static List<Spieler> ErstelleSpielerListe(List<Karte> karten)
    {
      var spieler = new List<Spieler>();
      for (int i = 0; i < 4; i++)
      {
        spieler.Add(new Spieler(VerteileKarten(karten)));
      }
      return spieler;
    }

    private static List<Karte> VerteileKarten(List<Karte> cards)
    {
      List<Karte> kartenFuerSpieler = new List<Karte>();
      for (int i = 0; i < 8; i++)
      {
        GibKarte(cards, kartenFuerSpieler);
      }
      return kartenFuerSpieler.ToList();
    }

    private static void GibKarte(List<Karte> cards, List<Karte> cardsForPlayer)
    {
      int cardIndex = ZufaelligeKartenNummer(cards);
      var cardToAdd = cards[cardIndex];
      cards.Remove(cardToAdd);
      cardsForPlayer.Add(cardToAdd);
    }

    private static int ZufaelligeKartenNummer(List<Karte> cards)
    {
      return new Random(System.DateTime.Now.Millisecond.GetHashCode()).Next(0, cards.Count() - 1);
    }

    #endregion

    


  }
}
