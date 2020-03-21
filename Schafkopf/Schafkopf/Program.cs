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
      SchafkopfGame schafkopf = new SchafkopfGame();
      PrintField(schafkopf);
      var spieler = SelectSpieler(schafkopf);
      var sau = SelectSau(schafkopf);
      Sauspiel spiel = new Sauspiel(schafkopf, spieler, sau);
    }

    private static void PrintField(SchafkopfGame spiel)
    {
      for (int i = 0; i < spiel.PlayerList.Count(); i++)
      {
        Console.Write("Spieler " + i + " :");
        foreach(var karte in spiel.PlayerList[i].Cards)
        {
          Console.Write(karte.CardValue + " ");
        }
        Console.WriteLine();
      }
    }

    private static Player SelectSpieler(SchafkopfGame spiel)
    {
      Console.Write("Spieler der Spielen möchte (0-3): ");
      int spielerIndex = int.Parse(Console.ReadLine());
      return spiel.PlayerList[spielerIndex];
    }

    private static CardValues SelectSau(SchafkopfGame spiel)
    {
      Console.Write("Sau auf die gespielt werden soll: (E/G/H/S): ");
      string sauFarbeInputLetter = Console.ReadLine();
      CardValues[] kartenWerte = new CardValues[] { CardValues.EA, CardValues.GA, CardValues.HA, CardValues.SA };
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
