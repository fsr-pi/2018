using System;

namespace Razred
{
  class Razred
  {
    public const int konst = 13; // konstanta razreda
    public int var = 0; // varijabla objekta
    public readonly int neDiraj; // postavlja se u konstruktoru
    public static int trajna = 0; // dijeljena između instanci

    public Razred()
    {
      Console.WriteLine("Standardni konstruktor");
      neDiraj = 0;
    }

    public Razred(int neDiraj0, int var0)
    {
      Console.WriteLine("Konstruktor s argumentima");
      neDiraj = neDiraj0;
      var = var0;
    }

    public int Zbroji(int a, int b)
    {
      return a + b;
    }

    public static int Oduzmi(int a, int b)
    {
      return a - b;
    }

    //Finalizator se piše u obliku destruktora
    ~Razred()
    {
      Console.WriteLine("Finalizer var={0} , neDiraj = {1}", var, neDiraj);
    }
  }
}
