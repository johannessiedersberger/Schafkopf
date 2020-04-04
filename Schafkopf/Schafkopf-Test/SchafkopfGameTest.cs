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
  }
}
