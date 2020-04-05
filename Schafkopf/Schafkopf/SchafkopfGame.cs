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

    /// <summary>
    /// Redistributes the cards to the stiche list of 
    /// the player with the highest card
    /// </summary>
    /// <param name="cards">All cards</param>
    /// <param name="highestCard">The highest Card</param>
    public void RedistributeStich(Card[] cards, Card highestCard)
    {
      if (cards.Contains(highestCard) == false)
        throw new ArgumentException("The highest card needs to be included in the cards array");

      var winner = highestCard.Owner;
      winner.Stiche.Add(cards);

      RemoveCardsFromPlayers(cards);
    }

    private void RemoveCardsFromPlayers(Card[] cards)
    {
      foreach(var card in cards)
      {
        foreach(var player in PlayerList)
        {
          if (player.Cards.Contains(card))
          {
            player.Cards.Remove(card);
          }
        }
      }
    }

    /// <summary>
    /// Returns the player index in the player list 
    /// </summary>
    /// <param name="player">The player to search for</param>
    /// <returns></returns>
    public int GetPlayerIndex(Player player)
    {
      for (int i = 0; i < PlayerList.Count(); i++)
      {
        if (PlayerList[i] == player)
          return i;
      }
      throw new Exception("Player not Found");
    }

    /// <summary>
    /// Counts all points in the stiche list of the players 
    /// </summary>
    /// <returns>A dictionary with the points of the players</returns>
    public Dictionary<Player, int> CountStichPointsByPlayers()
    {
      var points = new Dictionary<Player, int>();
      foreach(var player in PlayerList)
      {
        var playerPoints = CountPoints(player.Stiche.SelectMany(card => card).ToList());
        points.Add(player, playerPoints);
      }
      return points;
    }

    /// <summary>
    /// Counts the points of a card list
    /// </summary>
    /// <param name="cards">The cards to count the points</param>
    /// <returns></returns>
    public int CountPoints(List<Card> cards)
    {
      var schlaege = Extensions.AllSchlaege().ToList();
      schlaege.Reverse();
      var points = new List<int> { 0, 0, 0, 2, 3, 4, 10, 11 };

      int allPoints = 0;
      foreach (Card card in cards)
      {
        for (int i = 0; i < schlaege.Count(); i++)
        {
          if (card.SchlagValue == schlaege[i])
            allPoints += points[i];
        }
      }
      return allPoints;
    }
  }
}
