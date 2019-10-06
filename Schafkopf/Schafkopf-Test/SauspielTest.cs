using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Schafkopf_Wpf;

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
      players.Add(new Spieler(new List<Karte>() {
        new Karte(Kartenwerte.E10),
        new Karte(Kartenwerte.H7),
        new Karte(Kartenwerte.HA)
      }));
      SchafkopfSpiel game = new SchafkopfSpiel(players);
      // When / Then
      Assert.Throws<ArgumentException>(() => new Sauspiel(game, players[0], new Karte(Kartenwerte.H7)));
    }

    [Test]
    public void TestIfPlayerDoesNotHaveColor()
    {
      // Given
      List<Spieler> players = new List<Spieler>();
      players.Add(new Spieler(new List<Karte>() {
        new Karte(Kartenwerte.E10),
        new Karte(Kartenwerte.H7),
        new Karte(Kartenwerte.HA)
      }));
      SchafkopfSpiel game = new SchafkopfSpiel(players);
      // When / Then
      Assert.Throws<InvalidOperationException>(() => new Sauspiel(game, players[0], new Karte(Kartenwerte.HA)));
    }

    [Test]
    public void TestIfPlayerHasAssHimself()
    {
      // Given
      List<Spieler> players = new List<Spieler>();
      players.Add(new Spieler(new List<Karte>() {
        new Karte(Kartenwerte.E10),
        new Karte(Kartenwerte.H7),
        new Karte(Kartenwerte.HA)
      }));
      SchafkopfSpiel game = new SchafkopfSpiel(players);
      // When / Then
      Assert.Throws<InvalidOperationException>(() => new Sauspiel(game, players[0], new Karte(Kartenwerte.EA)));
    }

    [Test]
    public void TestConstruction()
    {
      // Given
      List<Spieler> players = new List<Spieler>();
      players.Add(new Spieler(new List<Karte>() { new Karte(Kartenwerte.E10), new Karte(Kartenwerte.H7), new Karte(Kartenwerte.H9),new Karte(Kartenwerte.EO)}));
      players.Add(new Spieler(new List<Karte>() { new Karte(Kartenwerte.E9), new Karte(Kartenwerte.E7), new Karte(Kartenwerte.E7), new Karte(Kartenwerte.EA) }));
      SchafkopfSpiel game = new SchafkopfSpiel(players);
      // When
      Sauspiel saupspiel = new Sauspiel(game, players[0], players[1].Karten[3]);
      // Then
      Assert.That(saupspiel.Spielmacher, Is.EqualTo(players[0]));
      Assert.That(saupspiel.SpielerPartner, Is.EqualTo(players[1]));
      Assert.That(saupspiel.GesuchtesAss.Kartenwert, Is.EqualTo(Kartenwerte.EA));
    }
    #endregion

    #region cardComparion

    [Test]
    public void TestHoechsteFarbe()
    {
      // Given
      List<Spieler> players = new List<Spieler>();
      players.Add(new Spieler(new List<Karte>() { new Karte(Kartenwerte.E7), new Karte(Kartenwerte.HO) }));
      players.Add(new Spieler(new List<Karte>() { new Karte(Kartenwerte.EA), new Karte(Kartenwerte.EO) }));
      SchafkopfSpiel game = new SchafkopfSpiel(players);
      
      Sauspiel saupspiel = new Sauspiel(game, players[0], players[1].Karten[0]);
      var spielKarten = new Karte[] {
        players[0].Karten[1],
        players[1].Karten[1]
      };
      // When
      Assert.That(saupspiel.HoechsteFarbe(spielKarten).Kartenwert, Is.EqualTo(Kartenwerte.EO));
    }

    [Test]
    public void TestHoechstePunkte()
    {
      // Given
      List<Spieler> players = new List<Spieler>();
      players.Add(new Spieler(new List<Karte>() { new Karte(Kartenwerte.E7), new Karte(Kartenwerte.S7) }));
      players.Add(new Spieler(new List<Karte>() { new Karte(Kartenwerte.EA), new Karte(Kartenwerte.S10) }));
      SchafkopfSpiel game = new SchafkopfSpiel(players);

      Sauspiel saupspiel = new Sauspiel(game, players[0], players[1].Karten[0]);
      var spielKarten = new Karte[] {
        players[0].Karten[1],
        players[1].Karten[1]
      };
      // When
      var hoechsteKarte = saupspiel.HoechstePunkte(spielKarten);
      Assert.That(hoechsteKarte.Kartenwert, Is.EqualTo(Kartenwerte.S10));
      Assert.That(hoechsteKarte.Eigentuemer, Is.EqualTo(players[1]));
    }

    #endregion
  }
}
