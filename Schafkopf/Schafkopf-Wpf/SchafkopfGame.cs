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
  public enum Card
  {
    //7 8 9 10 U O K A
    E7, E8, E9, E10, EU, EO, EK, EA, //Eichel
    G7, G8, G9, G10, GU, GO, GK, GA, //Gras
    H7, H8, H9, H10, HU, HO, HK, HA, //Herz
    S7, S8, S9, S10, SU, SO, SK, SA, //Schellen
  }

  /// <summary>
  /// The Game
  /// </summary>
  public class SchafkopfGame
  {
    /// <summary>
    /// Contains the Players
    /// </summary>
    public List<Player> Players { get; private set; }

    /// <summary>
    /// Creates the Players and distributes the cards
    /// </summary>
    public SchafkopfGame()
    {
      var cards = CreateAllCards();
      Players = CreatePlayers(cards);
    }

    /// <summary>
    /// Create game with predifined Players
    /// for Test Purposes
    /// </summary>
    /// <param name="players"></param>
    internal SchafkopfGame(List<Player> players)
    {
      Players = players;
    }

    #region CardDistribution
    private static List<Card> CreateAllCards()
    {
      return Enum.GetValues(typeof(Card)).Cast<Card>().ToList();
    }

    private static List<Player> CreatePlayers(List<Card> cards)
    {
      var players = new List<Player>();
      for (int i = 0; i < 4; i++)
      {
        players.Add(new Player(DistributeCards(cards)));
      }
      return players;
    }

    private static List<Card> DistributeCards(List<Card> cards)
    {
      List<Card> cardsForPlayer = new List<Card>();
      for (int i = 0; i < 8; i++)
      {
        GiveCard(cards, cardsForPlayer);
      }
      return cardsForPlayer.ToList();
    }

    private static void GiveCard(List<Card> cards, List<Card> cardsForPlayer)
    {
      int cardIndex = RandomCardNumber(cards);
      var cardToAdd = cards[cardIndex];
      cards.Remove(cardToAdd);
      cardsForPlayer.Add(cardToAdd);
    }

    private static int RandomCardNumber(List<Card> cards)
    {
      return new Random(System.DateTime.Now.Millisecond.GetHashCode()).Next(0, cards.Count() - 1);
    }

    #endregion

    


  }
}
