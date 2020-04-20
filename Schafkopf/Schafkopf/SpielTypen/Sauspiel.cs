using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schafkopf
{
  /// <summary>
  /// A Sauspiel where 2 players play
  /// against the others
  /// </summary>
  public class Sauspiel
  {
    /// <summary>
    /// The player that plays
    /// </summary>
    public Player ChiefPlayer { get; private set; }
    /// <summary>
    /// The partner of the chief player
    /// </summary>
    public Player ChiefPlayerPartner { get; private set; }
    /// <summary>
    /// The schafkopf game that contains the all cards and players
    /// </summary>
    public SchafkopfGame Game { get; private set; }
    /// <summary>
    /// The ass card for which is searched for
    /// </summary>
    public Card SearchedAss { get; private set; }

    /// <summary>
    /// Initializes a new Game
    /// </summary>
    /// <param name="game">The game</param>
    /// <param name="chiefPlayer">The chief player</param>
    /// <param name="assValue">The card value of the ass</param>
    public Sauspiel(SchafkopfGame game, Player chiefPlayer, CardValues assValue)
    {
      var ass = SearchCard(game, assValue);
      if (ass.SchlagValue != Schlag.Ass)
        throw new ArgumentException("Die Karte muss ein Ass sein");

      if (HasPlayerColor(chiefPlayer, ass) == false)
        throw new InvalidOperationException("Der Spieler muss die Farbe haben, nach der er sucht");

      if (chiefPlayer == SearchGamePartner(game, ass))
        throw new InvalidOperationException("Der Spieler hat das Ass selber, auf das er spielen will");

      Game = game;
      ChiefPlayer = chiefPlayer;
      ChiefPlayerPartner = SearchGamePartner(game, ass);
      SearchedAss = ass;
    }

    private static Card SearchCard(SchafkopfGame game, CardValues cardValue)
    {
      foreach (var player in game.PlayerList)
      {
        var searchCard = player.Cards.Where(k => k.CardValue == cardValue);
        if (searchCard.Count() != 0)
          return searchCard.First();
      }
      throw new InvalidOperationException("Card not Found");
    }

    private static bool HasPlayerColor(Player spielmacher, Card ass)
    {
      return spielmacher.Cards.Where(c => c.ColorValue == ass.ColorValue).Count() > 0;
    }

    private static Player SearchGamePartner(SchafkopfGame game, Card ass)
    {
      var result = game.PlayerList.Where(player => player.Cards.Contains(ass));

      if (result.Count() != 0)
        return result.ToList().First();
      else
        throw new InvalidOperationException("No Partner Found");
    }

    /// <summary>
    /// Returns the highest card
    /// </summary>
    /// <param name="cards">All cards</param>
    /// <param name="firstCard">The card played first</param>
    /// <returns></returns>
    public static Card CardComparison(Card[] cards, Card firstCard)
    {
      if (CheckSchlagFarbePassed(cards, firstCard) == false)
        throw new ArgumentException("The player has to pass the color played first, if he has it");

      Card tempKarte = null;

      for (int write = 0; write < cards.Length; write++)
      {
        for (int sort = 0; sort < cards.Length - 1; sort++)
        {
          if (cards[sort] != HighestCard(cards[sort], cards[sort + 1], firstCard))
          {
            tempKarte = cards[sort + 1];
            cards[sort + 1] = cards[sort];
            cards[sort] = tempKarte;
          }
        }
      }

      return cards[0]; // highest Karte
    }

    /// <summary>
    /// Checks if the player passes the right color 
    /// that was played first, if he owns it
    /// </summary>
    /// <param name="cards">All cards</param>
    /// <param name="firstCard">The first card</param>
    /// <returns></returns>
    public static bool CheckSchlagFarbePassed(Card[] cards, Card firstCard)
    {
      foreach (var card in cards)
      {
        var firstColor = firstCard.ColorValue;
        var currentColor = card.ColorValue;

        if (firstCard.CardValue != card.CardValue // current card is not the start card
          && IsNeededCard(firstCard, card) == false // did not pass needed card
          && HasPlayerNeededCard(firstCard, card.Owner)) // Player has needed card
        {
          return false;
        }
      }
      return true;
    }
                                      //E7            //EO
    private static bool IsNeededCard(Card firstCard, Card cardToCheck)
    {
      if (IsHerzOberUnter(firstCard))
      {
        return IsHerzOberUnter(cardToCheck);
      }
      else // Colors
      {
        return firstCard.ColorValue == cardToCheck.ColorValue && IsHerzOberUnter(cardToCheck) == false;
      }
    }

    private static bool HasPlayerNeededCard(Card firstCard, Player player)
    {
      if (IsHerzOberUnter(firstCard))
      {
        return player.Cards.Where(c => IsHerzOberUnter(c)).Count() > 0;
      }
      else // Color
      {
        return player.Cards.Where(c => c.ColorValue == firstCard.ColorValue).Count() > 0;
      }

    }
   
    private static bool IsHerzOberUnter(Card cardToCheck)
    {
      return cardToCheck.SchlagValue == Schlag.Ober || cardToCheck.SchlagValue == Schlag.Unter || cardToCheck.ColorValue == Color.Herz;
    }

    internal static Card HighestCard(Card card1, Card card2, Card firstCardPlayed) // 2 Karten
    {
      Card[] cards = new Card[] { card1, card2 };
      var ober = GetOber(cards);
      var unter = GetUnter(cards);
      var herzen = GetHerzen(cards);

      if (ober.Length > 0) // Ober
      {
        if (ober.Length == 2)
          return HighestColor(cards);
        else // nur 1 Ober
          return ober[0];
      }
      else if (unter.Length > 0) // Unter
      {
        if (unter.Length == 2)
          return HighestColor(cards);
        else // nur 1 Unter
          return unter[0];
      }
      else if (herzen.Length > 0) // Herz
      {
        if (herzen.Count() == 2)
          return HighestPoints(cards);
        else // Herzen == 1
          return herzen.First();
      }
      else // Andere Farben
      {
        if (cards[0].ColorValue == cards[1].ColorValue) // Gleiche Farbe
        {
          return HighestPoints(cards);
        }
        else // Ungleiche Farbe
        {
          var ersteKarteFarben = cards.Where(karte => karte.ColorValue == firstCardPlayed.ColorValue);

          if (ersteKarteFarben.Count() == 1)
            return ersteKarteFarben.First();

          return cards[0];
        }
      }
    }

    private static Card[] GetOber(Card[] cards)
    {
      return cards.Where(karte => karte.SchlagValue == Schlag.Ober).ToArray();
    }

    private static Card[] GetUnter(Card[] cards)
    {
      return cards.Where(karte => karte.SchlagValue == Schlag.Unter).ToArray();
    }

    private static Card[] GetHerzen(Card[] cards)
    {
      return cards.Where(karte => karte.ColorValue == Color.Herz).ToArray();
    }

    internal static Card HighestColor(Card[] cards)
    {
      if (cards.Length != 2)
        throw new ArgumentException("only 2 cards accepted");

      if (cards[0].ColorValue == cards[1].ColorValue)
        throw new ArgumentException("Gleiche Farben");

      Color[] colors = Extensions.AllColors();
      foreach (Color f in colors)
      {
        var result = cards.Where(card => card.ColorValue == f);
        if (result.Count() != 0)
          return result.First();
      }
      throw new InvalidOperationException("HoechsteFarbe not Found");
    }

    internal static Card HighestPoints(Card[] karten)
    {
      if (karten[0].SchlagValue == karten[1].SchlagValue)
        throw new ArgumentException("Gleiche Farben");

      if (karten.Length != 2)
        throw new ArgumentException("only 2 cards accepted");

      Schlag[] schlaege = Extensions.AllSchlaege();

      foreach (Schlag schlag in schlaege)
      {
        var result = karten.Where(karte => karte.SchlagValue == schlag);
        if (result.Count() != 0)
          return result.First();
      }
      throw new InvalidOperationException("Hoechste Punkte not Found");
    }
  }
}
