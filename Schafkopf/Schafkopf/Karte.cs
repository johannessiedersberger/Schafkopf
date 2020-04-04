using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schafkopf
{
  /// <summary>
  /// Contains the cards for the Game
  /// </summary>
  public enum CardValues
  {
    //7 8 9 10 U O K A
    E7, E8, E9, E10, EU, EO, EK, EA, // Eichel
    G7, G8, G9, G10, GU, GO, GK, GA, // Gras
    H7, H8, H9, H10, HU, HO, HK, HA, // Herz
    S7, S8, S9, S10, SU, SO, SK, SA, // Schellen
  }

  /// <summary>
  /// The colors
  /// </summary>
  public enum Color
  {
    Eichel,
    Gras,
    Herz,
    Schellen
  }

  /// <summary>
  /// The schlaege
  /// </summary>
  public enum Schlag
  {
    Sieben,
    Acht,
    Neun,
    Unter,
    Ober,
    Koenig,
    Zehner,
    Ass
  }

  /// <summary>
  /// A Card with the owner
  /// </summary>
  public class Card
  {
    /// <summary>
    /// The value of the cards
    /// </summary>
    public CardValues CardValue { get; }

    /// <summary>
    /// The color value
    /// </summary>
    public Color ColorValue => Farben(CardValue);

    /// <summary>
    /// The schlag value
    /// </summary>
    public Schlag SchlagValue => Schlag(CardValue);

    /// <summary>
    /// The current owner of the card
    /// </summary>
    public Player Owner { get; set; }

    /// <summary>
    /// Assigns the value of the card
    /// </summary>
    /// <param name="kartenwert"></param>
    public Card(CardValues kartenwert)
    {
      CardValue = kartenwert;
    }

    private Color Farben(CardValues card)
    {
      var cardId = card.ToString();

      if (cardId[0] == 'E')
        return Color.Eichel;
      if (cardId[0] == 'G')
        return Color.Gras;
      if (cardId[0] == 'H')
        return Color.Herz;
      if (cardId[0] == 'S')
        return Color.Schellen;

      throw new ArgumentException("Wrong Card");
    }

   
    private static Schlag Schlag(CardValues card)
    {
      var cardString = card.ToString();

      char[] cardList = { '7', '8', '9', 'U', 'O', 'K', '1', 'A' };
      Schlag[] schlaege = Enum.GetValues(typeof(Schlag)).Cast<Schlag>().ToArray();

      for (int c = 0; c < cardList.Length; c++)
      {
        if (cardString[1] == cardList[c])
          return schlaege[c];
      }
      throw new InvalidOperationException("Schlag nicht gefunden");
    }


  }

  
}
