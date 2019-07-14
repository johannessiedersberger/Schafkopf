using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schafkopf
{
  public enum Color
  {
    Eichel,
    Gras,
    Herz,
    Schellen
  }
  public static class Extensions
  {
    public static IList<T> Clone<T>(this IList<T> listToClone) where T : ICloneable
    {
      return listToClone.Select(item => (T)item.Clone()).ToList();
    }

    public static Color GetColor(this Card card)
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
  }
}
