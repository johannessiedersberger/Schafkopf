using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schafkopf
{
  class Program
  {
    static void Main(string[] args)
    {
      var game = new SchafkopfGame();
      var sauspiel = new Sauspiel(game, game.Players[0], Card.EA);
      DisplayGame(game);
      var v = game.Players.First().Cards.First().GetColor();
    }

    static void DisplayGame(SchafkopfGame game)
    {
      foreach (var player in game.Players)
      {
        Console.Write("Player : ");
        foreach (var card in player.Cards)
        {
          Console.Write(card.ToString() + " ");
        }
        Console.WriteLine();
      }
    }
  }
}
