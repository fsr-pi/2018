using System;

namespace Barikade
{
  class Program
  {

    static void Main(string[] args)
    {
      Pitagora p = new Pitagora();
      //Console.WriteLine(p.Korijen(-5)); //što se događa kad ovo otkomentiramo (Debug? , Release?)
      p.A = -3;
      p.SetBFromString("abc");
      Console.WriteLine("Katete: {0}, {1}, hipotenuza {2}", p.A, p.B, p.C);
    }
  }
}