﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schafkopf_Wpf
{
  
  public static class Extensions
  {
    public static IList<T> Clone<T>(this IList<T> listToClone) where T : ICloneable
    {
      return listToClone.Select(item => (T)item.Clone()).ToList();
    }

    public static Farbe[] AlleFarben()
    {
      return Enum.GetValues(typeof(Farbe)).Cast<Farbe>().ToArray();
    }

    public static Schlag[] AlleSchlaege()
    {
      return Enum.GetValues(typeof(Schlag)).Cast<Schlag>().ToArray().Reverse().ToArray();
    }

    public static bool IsAny<T>(this IEnumerable<T> data)
    {
      return data != null && data.Any();
    }
  }
}
