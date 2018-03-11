using System;

namespace Barikade
{
  class Program
  {
    class Primjer
    {
      private int index; //privatna varijabla (iza barikade)

      public Primjer()
      { }

      public int Index //javna metoda (sluzi kao interface barikada)
      {
        get
        {
          return index;
        }
        set
        {
          //provjeravamo je li unutar dozvoljenih vrijednosti
          //ako nije, pridjeljujemo joj najblizu dozvoljenu vrijednost
          if (value <= 0) { this.index = 0; }
          else if (value > 100) { this.index = 100; }
          else { this.index = value; }
        }
      }
    }

    static void Main(string[] args)
    {
      Primjer p = new Primjer();
      p.Index = 200;
      Console.WriteLine("Index = {0}", p.Index);

    }
  }
}