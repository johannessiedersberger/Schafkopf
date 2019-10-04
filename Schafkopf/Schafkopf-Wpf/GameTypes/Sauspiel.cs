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
      if (ass.ToString().Contains('A') == false)
        throw new ArgumentException("You have to pass a ass");
      if (HasPlayerColor(spielmacher, ass) == false)
        throw new InvalidOperationException("The player needs the color of the ass");
      if (spielmacher == FindGamePartner(game, ass))
        throw new InvalidOperationException("The player has the ass he wants to play with himself");
      

      Game = game;
      Spielmacher = spielmacher;
      SpielerPartner = FindGamePartner(game, ass);
      AssCardToPlay = ass;
    }

    private static bool HasPlayerColor(Player spielmacher, Card ass)
    {
      return spielmacher.Cards.Where(c => c.GetColor() == ass.GetColor()).Count() > 0;
    }

    private static Player FindGamePartner(SchafkopfGame game, Card ass)
    {
      var result = game.Players.Where(player => player.Cards.Contains(ass));

      if (result.Count() != 0)
        return result.ToList().First();
      else
        throw new InvalidOperationException("No Partner Found");
    }

    

    
  }
}
