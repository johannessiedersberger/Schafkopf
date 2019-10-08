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
      var spieler = SelectSpieler(schafkopf);
      var sau = SelectSau(schafkopf);
      Sauspiel spiel = new Sauspiel(schafkopf, spieler, sau);
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

    private static Spieler SelectSpieler(SchafkopfSpiel spiel)
    {
      Console.Write("Spieler der Spielen möchte (0-3): ");
      int spielerIndex = int.Parse(Console.ReadLine());
      return spiel.SpielerListe[spielerIndex];
    }

    private static Kartenwerte SelectSau(SchafkopfSpiel spiel)
    {
      Console.Write("Sau auf die gespielt werden soll: (E/G/H/S): ");
      string sauFarbeInputLetter = Console.ReadLine();
      Kartenwerte[] kartenWerte = new Kartenwerte[] { Kartenwerte.EA, Kartenwerte.GA, Kartenwerte.HA, Kartenwerte.SA };
      string[] buchStabenWerte = new string[] { "E", "G", "H", "S" };

      for (int i = 0; i < buchStabenWerte.Length; i++)
      {
        if(sauFarbeInputLetter == buchStabenWerte[i])
        {
          return kartenWerte[i];
        }
      }

      throw new KeyNotFoundException("Sau nicht gefunden");
    }

    
  }
}
