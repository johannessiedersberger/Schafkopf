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
      players.Add(new Player(new List<Card>() {
        new Card(CardValues.E10),
        new Card(CardValues.H7),
        new Card(CardValues.HA)
      }));
      SchafkopfGame game = new SchafkopfGame(players);
      // When / Then
      Assert.Throws<ArgumentException>(() => new Sauspiel(game, players[0], CardValues.H7));
    }

    [Test]
    public void TestIfPlayerDoesNotHaveColor()
    {
      // Given
      List<Player> players = new List<Player>();
      players.Add(new Player(new List<Card>() {
        new Card(CardValues.E10),
        new Card(CardValues.H7),
        new Card(CardValues.HA)
      }));
      SchafkopfGame game = new SchafkopfGame(players);
      // When / Then
      Assert.Throws<InvalidOperationException>(() => new Sauspiel(game, players[0], CardValues.HA));
    }

    [Test]
    public void TestIfPlayerHasAssHimself()
    {
      // Given
      List<Player> players = new List<Player>();
      players.Add(new Player(new List<Card>() {
        new Card(CardValues.E10),
        new Card(CardValues.H7),
        new Card(CardValues.HA)
      }));
      SchafkopfGame game = new SchafkopfGame(players);
      // When / Then
      Assert.Throws<InvalidOperationException>(() => new Sauspiel(game, players[0], CardValues.EA));
    }

    [Test]
    public void TestConstruction()
    {
      // Given
      List<Player> players = new List<Player>();
      players.Add(new Player(new List<Card>() { new Card(CardValues.E10), new Card(CardValues.H7), new Card(CardValues.H9),new Card(CardValues.EO)}));
      players.Add(new Player(new List<Card>() { new Card(CardValues.E9), new Card(CardValues.E7), new Card(CardValues.E7), new Card(CardValues.EA) }));
      SchafkopfGame game = new SchafkopfGame(players);
      // When
      Sauspiel saupspiel = new Sauspiel(game, players[0], players[1].Cards[3].CardValue);
      // Then
      Assert.That(saupspiel.ChiefPlayer, Is.EqualTo(players[0]));
      Assert.That(saupspiel.ChiefPlayerPartner, Is.EqualTo(players[1]));
      Assert.That(saupspiel.SearchedAss.CardValue, Is.EqualTo(CardValues.EA));
    }
    #endregion

    #region cardComparion

    [Test]
    public void TestHighestColor()
    {
      // Given
      List<Player> players = new List<Player>();
      players.Add(new Player(new List<Card>() { new Card(CardValues.E7), new Card(CardValues.HO) }));
      players.Add(new Player(new List<Card>() { new Card(CardValues.EA), new Card(CardValues.EO) }));
      SchafkopfGame game = new SchafkopfGame(players);
      
      Sauspiel saupspiel = new Sauspiel(game, players[0], players[1].Cards[0].CardValue);
      var spielKarten = new Card[] {
        players[0].Cards[1],
        players[1].Cards[1]
      };
      // When
      Assert.That(Sauspiel.HighestColor(spielKarten).CardValue, Is.EqualTo(CardValues.EO));
    }

    [Test]
    public void TestHighestPoints()
    {
      // Given
      List<Player> players = new List<Player>();
      players.Add(new Player(new List<Card>() { new Card(CardValues.E7), new Card(CardValues.S7) }));
      players.Add(new Player(new List<Card>() { new Card(CardValues.EA), new Card(CardValues.S10) }));
      SchafkopfGame game = new SchafkopfGame(players);

      Sauspiel saupspiel = new Sauspiel(game, players[0], players[1].Cards[0].CardValue);
      var spielKarten = new Card[] {
        players[0].Cards[1],
        players[1].Cards[1]
      };
      // When
      var hoechsteKarte = Sauspiel.HighestPoints(spielKarten);
      Assert.That(hoechsteKarte.CardValue, Is.EqualTo(CardValues.S10));
      Assert.That(hoechsteKarte.Owner, Is.EqualTo(players[1]));
    }

    [Test]
    public void TestEichelOber()
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
      // When
      var hoechsteKarte = Sauspiel.CardComparison(spielKarten, players[0].Cards[0]);
      Assert.That(hoechsteKarte.CardValue, Is.EqualTo(CardValues.EO));
      Assert.That(hoechsteKarte.Owner, Is.EqualTo(players[0]));
    }

    [Test]
    public void TestHerz()
    {
      // Given
      List<Player> players = new List<Player>();
      players.Add(new Player(new List<Card>() { new Card(CardValues.G7) }));
      players.Add(new Player(new List<Card>() { new Card(CardValues.H7) }));
      players.Add(new Player(new List<Card>() { new Card(CardValues.E7) }));
      players.Add(new Player(new List<Card>() { new Card(CardValues.GA) }));
      SchafkopfGame game = new SchafkopfGame(players);

      Sauspiel saupspiel = new Sauspiel(game, players[0], players[3].Cards[0].CardValue);
      var spielKarten = new Card[] {
        players[0].Cards[0],
        players[1].Cards[0],
        players[2].Cards[0],
        players[3].Cards[0]
      };
      // When
      var hoechsteKarte = Sauspiel.CardComparison(spielKarten, players[0].Cards[0]);
      Assert.That(hoechsteKarte.CardValue, Is.EqualTo(CardValues.H7));
      Assert.That(hoechsteKarte.Owner, Is.EqualTo(players[1]));
    }

    [Test]
    public void TestOberFarbe()
    {
      // Given
      List<Player> players = new List<Player>();
      players.Add(new Player(new List<Card>() { new Card(CardValues.G7) }));
      players.Add(new Player(new List<Card>() { new Card(CardValues.HO) }));
      players.Add(new Player(new List<Card>() { new Card(CardValues.SO) }));
      players.Add(new Player(new List<Card>() { new Card(CardValues.GA) }));
      SchafkopfGame game = new SchafkopfGame(players);

      Sauspiel saupspiel = new Sauspiel(game, players[0], players[3].Cards[0].CardValue);
      var spielKarten = new Card[] {
        players[0].Cards[0],
        players[1].Cards[0],
        players[2].Cards[0],
        players[3].Cards[0]
      };
      // When
      var hoechsteKarte = Sauspiel.CardComparison(spielKarten, players[0].Cards[0]);
      Assert.That(hoechsteKarte.CardValue, Is.EqualTo(CardValues.HO));
      Assert.That(hoechsteKarte.Owner, Is.EqualTo(players[1]));
    }

    [Test]
    public void TestErsteFarbeGespielt()
    {
      // Given
      List<Player> players = new List<Player>();
      players.Add(new Player(new List<Card>() { new Card(CardValues.G7) }));
      players.Add(new Player(new List<Card>() { new Card(CardValues.S8) }));
      players.Add(new Player(new List<Card>() { new Card(CardValues.S7) }));
      players.Add(new Player(new List<Card>() { new Card(CardValues.GA) }));
      SchafkopfGame game = new SchafkopfGame(players);

      Sauspiel saupspiel = new Sauspiel(game, players[0], players[3].Cards[0].CardValue);
      var spielKarten = new Card[] {
        players[0].Cards[0],
        players[1].Cards[0],
        players[2].Cards[0],
        players[3].Cards[0]
      };
      // When
      var hoechsteKarte = Sauspiel.CardComparison(spielKarten, players[1].Cards[0]);
      Assert.That(hoechsteKarte.CardValue, Is.EqualTo(CardValues.S8));
      Assert.That(hoechsteKarte.Owner, Is.EqualTo(players[1]));
    }

    [Test]
    public void TestPassColorCorrect()
    {
      // Given
      List<Player> players = new List<Player>();
      players.Add(new Player(new List<Card>() { new Card(CardValues.G7) }));
      players.Add(new Player(new List<Card>() { new Card(CardValues.S8) }));
      players.Add(new Player(new List<Card>() { new Card(CardValues.S7) }));
      players.Add(new Player(new List<Card>() { new Card(CardValues.GA) }));
      SchafkopfGame game = new SchafkopfGame(players);

      Sauspiel saupspiel = new Sauspiel(game, players[0], players[3].Cards[0].CardValue);
      var spielKarten = new Card[] {
        players[0].Cards[0],
        players[1].Cards[0],
        players[2].Cards[0],
        players[3].Cards[0]
      };
      // When / Then 
      Assert.That(Sauspiel.CheckSchlagFarbePassed(spielKarten, spielKarten[0]));
    }

    [Test]
    public void TestPassColorFalse()
    {
      // Given
      List<Player> players = new List<Player>();
      players.Add(new Player(new List<Card>() { new Card(CardValues.G7) }));
      players.Add(new Player(new List<Card>() { new Card(CardValues.S8), new Card(CardValues.G9) }));
      players.Add(new Player(new List<Card>() { new Card(CardValues.S7) }));
      players.Add(new Player(new List<Card>() { new Card(CardValues.GA) }));
      SchafkopfGame game = new SchafkopfGame(players);

      Sauspiel saupspiel = new Sauspiel(game, players[0], players[3].Cards[0].CardValue);
      var spielKarten = new Card[] {
        players[0].Cards[0],
        players[1].Cards[0],
        players[2].Cards[0],
        players[3].Cards[0]
      };
      // When / Then 
      Assert.That(Sauspiel.CheckSchlagFarbePassed(spielKarten, spielKarten[0]) == false);
      Assert.Throws<ArgumentException>(() => Sauspiel.CardComparison(spielKarten, spielKarten[0]));
    }

    #endregion
  }
}
