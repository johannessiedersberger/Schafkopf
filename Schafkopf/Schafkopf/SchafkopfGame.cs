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
  public class SchafkopfGame
  {
    /// <summary>
    /// Contains the Players
    /// </summary>
    public List<Player> PlayerList { get; private set; }

    /// <summary>
    /// Creates the Players and distributes the cards
    /// </summary>
    public SchafkopfGame()
    {
      var cards = CreateAllCards();
      PlayerList = CreatePlayerList(cards);
    }

    /// <summary>
    /// Create game with predifined Players
    /// for Test Purposes
    /// </summary>
    /// <param name="spieler"></param>
    internal SchafkopfGame(List<Player> spieler)
    {
      PlayerList = spieler;
    }

    #region CardDistribution
    private static List<CardValues> CreateAllCards()
    {
      return Enum.GetValues(typeof(CardValues)).Cast<CardValues>().ToList();
    }

    private static List<Player> CreatePlayerList(List<CardValues> cardValues)
    {
      var player = new List<Player>();
      for (int i = 0; i < 4; i++)
      {
        player.Add(new Player(DistributeCards(cardValues)));
      }
      return player;
    }

    private static List<Card> DistributeCards(List<CardValues> cardValues)
    {
      List<Card> cardsForPlayers = new List<Card>();
      for (int i = 0; i < 8; i++)
      {
        GiveCard(cardValues, cardsForPlayers);
      }
      return cardsForPlayers.ToList();
    }

    private static void GiveCard(List<CardValues> cardValues, List<Card> cardsForPlayer)
    {
      int cardIndex = RandomCardNUmber(cardValues);
      var cardValue = cardValues[cardIndex];
      cardValues.Remove(cardValue);
      cardsForPlayer.Add(new Card(cardValue));
    }

    private static int RandomCardNUmber(List<CardValues> cards)
    {
      return new Random(System.DateTime.Now.Millisecond.GetHashCode()).Next(0, cards.Count() - 1);
    }

    #endregion

    /// <summary>
    /// Returns the card object that contains the value searched for
    /// </summary>
    /// <param name="value">The value of the card</param>
    /// <returns></returns>
    public Card GetCardbyValue(CardValues value)
    {
      Card searchedCard = null;
      foreach(var player in PlayerList)
      {
        var cards = player.Cards.Where(card => card.CardValue == value);
        if (cards.Any())
          searchedCard = cards.First();

        foreach(var stich in player.Stiche)
        {
          var s = stich.Where(card => card.CardValue == value);
          if (s.Any())
            searchedCard = s.First();
        }
      }

      if (searchedCard == null)
        throw new Exception("Card not found");
      else
        return searchedCard;
    }


  }
}
