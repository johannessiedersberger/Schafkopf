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
      SchafkopfSpiel schafkopf = new SchafkopfSpiel();
      PrintField(schafkopf);
    }

    private static void PrintField(SchafkopfSpiel spiel)
    {
      for (int i = 0; i < spiel.SpielerListe.Count(); i++)
      {
        Console.Write("Spieler " + i + " :");
        foreach(var karte in spiel.SpielerListe[i].Karten)
        {
          Console.Write(karte.Kartenwert + " ");
        }
        Console.WriteLine();
      }
    }

    
  }
}
