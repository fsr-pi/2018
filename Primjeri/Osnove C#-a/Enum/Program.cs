using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Enum
{
  public class Program
  {
    public enum Dani
    {
      Ponedjeljak, Utorak, Srijeda,
      Cetvrtak, Petak, Subota, Nedjelja
    }

    public enum DaniPoRedu
    {
      Ponedjeljak = 1, Utorak, Srijeda,
      Cetvrtak, Petak, Subota, Nedjelja
    }


    static public void Main()
    {
      DaniPoRedu dan1 = DaniPoRedu.Ponedjeljak;
      DaniPoRedu dan2 = DaniPoRedu.Utorak;
      DaniPoRedu dan3 = DaniPoRedu.Ponedjeljak;

      if (dan1 == dan3 && dan2 == DaniPoRedu.Utorak)
        Console.WriteLine("Uvjet zadovoljen");

      //mogu se pretvoriti u int ako treba, ali najčešće se to ne radi
      int x = (int)Dani.Ponedjeljak;
      int y = (int)Dani.Utorak;

      Console.WriteLine("Pon = {0}", x);
      Console.WriteLine("Uto = {0}", y);
      
      Console.WriteLine("{0} je {1:D}. dan u tjednu",
          DaniPoRedu.Utorak, DaniPoRedu.Utorak);
    }
  }
}
