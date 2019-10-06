using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Schafkopf_Wpf
{
  class MainViewModel
  {
    public SchafkopfSpiel SchafkopfSpiel;

    public MainViewModel()
    {
      SchafkopfSpiel = new SchafkopfSpiel();
      Sauspiel sauspiel = new Sauspiel(SchafkopfSpiel, SchafkopfSpiel.SpielerListe[0], new Karte(Kartenwerte.EA));
      
    }
  }
}
