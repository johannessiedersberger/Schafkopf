using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schafkopf
{
  /// <summary>
  /// A player in the schafkopf game
  /// </summary>
  public class Player
  {
    /// <summary>
    /// All stiche the player has made
    /// </summary>
    public List<Card[]> Stiche { get; }
    
    /// <summary>
    /// All cards the player holds
    /// </summary>
    public List<Card> Cards { get; }
   
    /// <summary>
    /// Assigns the cards to the player
    /// </summary>
    /// <param name="karten">The cards to assign</param>
    public Player(List<Card> karten)
    {
      Cards = karten;
      SetOwner(karten, this);
    }

    private static void SetOwner(List<Card> karten, Player spieler)
    {
      foreach(var karte in karten)
      {
        karte.Owner = spieler;
      }
    }
  }
}
