using System;

namespace Kompozicija
{
  class Program
  {
    static void Main(string[] args)
    {
      Osoba o = new Osoba("1234567890123", "Unska 3", "Vukovarska 23", "Tvrtko", "Bajs");

      Console.WriteLine(o.ToString());
    }
  }
}