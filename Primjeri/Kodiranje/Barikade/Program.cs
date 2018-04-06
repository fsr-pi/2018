using System;
using System.Text;

namespace Barikade
{
  class Program
  {
    static void Main(string[] args)
    {
      Console.OutputEncoding = Encoding.UTF8;

      Predmet p = new Predmet("RPPP");
      p["Pero"] = 2;
      p["Ana"] = 5;
      p["Ivan"] = 5;
      p["Marko"] = 1;
      p["Luka"] = 3;
      //p["Marija"] = -7; //treba uzrokovati iznimku 
      Console.WriteLine("Prosječna ocjena na predmetu: " + p.ProsjecnaOcjena());
      Console.WriteLine("Ante ima ocjenu: " + p["Ante"]); //ispisat će -1
    }
  }
}