using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schafkopf
{
  public class Sauspiel
  {
    public Player Spielmacher { get; private set; }
    public Player SpielerPartner { get; private set; }
    public SchafkopfGame Game { get; private set; }
    public Card AssCardToPlay { get; private set; }

    public Sauspiel(SchafkopfGame game, Player spielmacher, Card ass)
    {
      if (CheckForColor(spielmacher, ass))
        throw new InvalidOperationException("The player needs the color of the ass");

      Game = game;
      Spielmacher = spielmacher;
      AssCardToPlay = ass;
    }

    private static bool CheckForColor(Player spielmacher, Card ass)
    {
      return spielmacher.Cards.Where(c => c.GetColor() == ass.GetColor()).Count() > 0;
    }

    private static Player FindGamePartner(SchafkopfGame game, Card ass)
    {
      return game.Players.Where(player => player.Cards.Contains(ass)).ToList().First();
    }

    
  }
}
