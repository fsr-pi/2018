using System;
using System.Diagnostics;

namespace Assert
{
  class Program
  {
    public static int Brzina(int sirina, int duzina, int visina)
    {
      Debug.Assert((-90 <= sirina) && (sirina <= 90), "Neispravna sirina.");
      Debug.Assert((0 <= duzina) && (duzina < 360), "Neispravna duzina.");
      Debug.Assert((-500 <= visina) && (visina <= 75000), "Neispravna visina.");      
      //...
      //racun
      //...

      int brzina = 650;
      Debug.Assert((0 <= brzina) && (brzina <= 600), "Neispravna brzina.");

      return brzina;
    }

    static void Main(string[] args)
    {
      int vrijednost = Brzina(0, 0, 0);
      Console.WriteLine(vrijednost);
    }
  }
}