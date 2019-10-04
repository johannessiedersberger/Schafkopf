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
      List<Spieler> players = new List<Spieler>();
      players.Add(new Spieler(new List<Karte>() { Karte.E10, Karte.H7, Karte.HA }));
      SchafkopfSpiel game = new SchafkopfSpiel(players);
      // When / Then
      Assert.Throws<ArgumentException>(() => new Sauspiel(game, players[0], Karte.E7));
    }

    [Test]
    public void TestIfPlayerDoesNotHaveColor()
    {
      // Given
      List<Spieler> players = new List<Spieler>();
      players.Add(new Spieler(new List<Karte>() { Karte.E10, Karte.H7, Karte.HA }));
      SchafkopfSpiel game = new SchafkopfSpiel(players);
      // When / Then
      Assert.Throws<InvalidOperationException>(() => new Sauspiel(game, players[0], Karte.HA));
    }

    [Test]
    public void TestIfPlayerHasAssHimself()
    {
      // Given
      List<Spieler> players = new List<Spieler>();
      players.Add(new Spieler(new List<Karte>() { Karte.E10, Karte.H7, Karte.HA }));
      SchafkopfSpiel game = new SchafkopfSpiel(players);
      // When / Then
      Assert.Throws<InvalidOperationException>(() => new Sauspiel(game, players[0], Karte.HA));
    }

    [Test]
    public void TestConstruction()
    {
      // Given
      List<Spieler> players = new List<Spieler>();
      players.Add(new Spieler(new List<Karte>() { Karte.E10, Karte.H7, Karte.H9, Karte.EO}));
      players.Add(new Spieler(new List<Karte>() { Karte.E9, Karte.E7, Karte.E7, Karte.EA }));
      SchafkopfSpiel game = new SchafkopfSpiel(players);
      // When
      Sauspiel saupspiel = new Sauspiel(game, players[0], Karte.EA);
      // Then
      Assert.That(saupspiel.Spielmacher, Is.EqualTo(players[0]));
      Assert.That(saupspiel.SpielerPartner, Is.EqualTo(players[1]));
      Assert.That(saupspiel.GesuchtesAss, Is.EqualTo(Karte.EA));
    }
    #endregion

    
  }
}
