using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schafkopf
{
  /// <summary>
  /// Extension Methods for the schafkopf game
  /// </summary>
  public static class Extensions
  {
    /// <summary>
    /// Clones a list
    /// </summary>
    /// <typeparam name="T">The list type</typeparam>
    /// <param name="listToClone">The list to clone</param>
    /// <returns></returns>
    public static IList<T> Clone<T>(this IList<T> listToClone) where T : ICloneable
    {
      return listToClone.Select(item => (T)item.Clone()).ToList();
    }

    /// <summary>
    /// Returns a list of all Colors
    /// </summary>
    /// <returns></returns>
    public static Color[] AllColors()
    {
      return Enum.GetValues(typeof(Color)).Cast<Color>().ToArray();
    }

    /// <summary>
    /// Returns an Array of all schlaege (7,8,9,10,U,O,K,A)
    /// </summary>
    /// <returns></returns>
    public static Schlag[] AllSchlaege()
    {
      return Enum.GetValues(typeof(Schlag)).Cast<Schlag>().ToArray().Reverse().ToArray();
    }

    /// <summary>
    /// Returns a random card value
    /// </summary>
    /// <returns></returns>
    public static CardValues RandomCardValue()
    {
      Random random = new Random();
      int randomIndex = random.Next(0, 32);
      return Enum.GetValues(typeof(CardValues)).Cast<CardValues>().ToArray()[randomIndex];
    }

    /// <summary>
    /// Check if sequence contains elements
    /// </summary>
    /// <typeparam name="T">The type</typeparam>
    /// <param name="data">The Enumerable to check</param>
    /// <returns></returns>
    public static bool IsAny<T>(this IEnumerable<T> data)
    {
      return data != null && data.Any();
    }
  }
}
