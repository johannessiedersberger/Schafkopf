using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Schafkopf;

namespace Schafkopf_Test
{
  class SauspielTest
  {
    #region construction
    [Test]
    public void TestIfAss()
    {
      // Given
      List<Player> players = new List<Player>();
      players.Add(new Player(new List<Card>() { Card.E10, Card.H7, Card.HA }));
      SchafkopfGame game = new SchafkopfGame(players);
      // When / Then
      Assert.Throws<ArgumentException>(() => new Sauspiel(game, players[0], Card.E7));
    }

    [Test]
    public void TestIfPlayerDoesNotHaveColor()
    {
      // Given
      List<Player> players = new List<Player>();
      players.Add(new Player(new List<Card>() { Card.E10, Card.H7, Card.HA }));
      SchafkopfGame game = new SchafkopfGame(players);
      // When / Then
      Assert.Throws<InvalidOperationException>(() => new Sauspiel(game, players[0], Card.HA));
    }

    [Test]
    public void TestIfPlayerHasAssHimself()
    {
      // Given
      List<Player> players = new List<Player>();
      players.Add(new Player(new List<Card>() { Card.E10, Card.H7, Card.HA }));
      SchafkopfGame game = new SchafkopfGame(players);
      // When / Then
      Assert.Throws<InvalidOperationException>(() => new Sauspiel(game, players[0], Card.HA));
    }

    [Test]
    public void TestConstruction()
    {
      // Given
      List<Player> players = new List<Player>();
      players.Add(new Player(new List<Card>() { Card.E10, Card.H7, Card.H9, Card.EO}));
      players.Add(new Player(new List<Card>() { Card.E9, Card.E7, Card.E7, Card.EA }));
      SchafkopfGame game = new SchafkopfGame(players);
      // When
      Sauspiel saupspiel = new Sauspiel(game, players[0], Card.EA);
      // Then
      Assert.That(saupspiel.Spielmacher, Is.EqualTo(players[0]));
      Assert.That(saupspiel.SpielerPartner, Is.EqualTo(players[1]));
      Assert.That(saupspiel.AssCardToPlay, Is.EqualTo(Card.EA));
    }
    #endregion

    
  }
}
