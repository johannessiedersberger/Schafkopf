using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schafkopf
{
  
  public static class Extensions
  {
    public static IList<T> Clone<T>(this IList<T> listToClone) where T : ICloneable
    {
      return listToClone.Select(item => (T)item.Clone()).ToList();
    }

    public static Color[] AllColors()
    {
      return Enum.GetValues(typeof(Color)).Cast<Color>().ToArray();
    }

    public static Schlag[] AllSchlaege()
    {
      return Enum.GetValues(typeof(Schlag)).Cast<Schlag>().ToArray().Reverse().ToArray();
    }

    public static CardValues RandomCardValue()
    {
      Random random = new Random();
      int randomIndex = random.Next(0, 32);
      return Enum.GetValues(typeof(CardValues)).Cast<CardValues>().ToArray()[randomIndex];
    }

    public static bool IsAny<T>(this IEnumerable<T> data)
    {
      return data != null && data.Any();
    }
  }
}
