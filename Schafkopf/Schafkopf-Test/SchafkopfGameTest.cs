using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Schafkopf;

namespace Schafkopf_Test
{
  class SchafkopfGameTest
  {
    [Test]
    public void TestNumberOfPlayers()
    {
      //Given // When
      var game = new SchafkopfGame();
      //Then
      Assert.That(game.PlayerList.Count, Is.EqualTo(4));     
    }

    [Test]
    public void TestNumberOfCards()
    {
      //Given
      var game = new SchafkopfGame();
      //Then
      foreach(var player in game.PlayerList)
        Assert.That(player.Cards.Count(), Is.EqualTo(8));      
    }

    [Test]
    public void CheckIfCardDuplicated()
    {
      // Given
      var game = new SchafkopfGame();
      // When
      var allCards = game.PlayerList.SelectMany(d => d.Cards).ToList();
      // Then
      foreach(var card in allCards)
      {
        var h = new HashSet<int>();
        Assert.That(h.Any(x => !h.Add(x)), Is.EqualTo(false));
      }
    }

    [Test]
    public void TestGetCardByValue()
    {
      //Given 
      var game = new SchafkopfGame();
      // When
      var card = game.GetCardbyValue(CardValues.E10);
      // Then
      Assert.That(card.CardValue, Is.EqualTo(CardValues.E10));
      Assert.That(card.Owner, Is.Not.Null);
    }

    [Test]
    public void TestRedistributeStich()
    {
      // Given
      List<Player> players = new List<Player>();
      players.Add(new Player(new List<Card>() { new Card(CardValues.EO) }));
      players.Add(new Player(new List<Card>() { new Card(CardValues.H7) }));
      players.Add(new Player(new List<Card>() { new Card(CardValues.E7) }));
      players.Add(new Player(new List<Card>() { new Card(CardValues.EA) }));
      SchafkopfGame game = new SchafkopfGame(players);

      Sauspiel saupspiel = new Sauspiel(game, players[0], players[3].Cards[0].CardValue);
      var spielKarten = new Card[] {
        players[0].Cards[0],
        players[1].Cards[0],
        players[2].Cards[0],
        players[3].Cards[0]
      };

      var hoechsteKarte = Sauspiel.CardComparison(spielKarten, players[0].Cards[0]);

      // When
      game.RedistributeStich(spielKarten, hoechsteKarte);

      // Then
      Assert.That(hoechsteKarte.CardValue, Is.EqualTo(CardValues.EO));
      Assert.That(hoechsteKarte.Owner, Is.EqualTo(players[0]));
      Assert.That(players[0].Stiche.Count(), Is.EqualTo(1));

      foreach(var player in game.PlayerList)
        Assert.That(player.Cards.Count(), Is.EqualTo(0));

      for (int i = 1; i <= 3; i++)
        Assert.That(players[i].Stiche.Count(), Is.EqualTo(0));
      
    }
  }
}
