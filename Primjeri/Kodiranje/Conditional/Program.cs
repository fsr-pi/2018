#define RAZVOJ2 //definiramo simbol (može i kroz Properties -> Build -> Conditional compilation symbols)
using System;
using System.Diagnostics;

namespace Conditional
{
  class Program
  {
    static void Main(string[] args)
    {

#if (RAZVOJ)

			//...
			// kod za debugiranje
			//...

			Console.WriteLine("Poruka UNUTAR koda za debugiranje!");
			Console.WriteLine();			
#endif

      Test();
      Console.WriteLine();

      //...
      Console.WriteLine("Poruka IZVAN koda za debugiranje!");

      Console.WriteLine("\nPress enter!");
      Console.ReadLine();
    }

    [Conditional("RAZVOJ")]
    static void Test()
    {
      Console.WriteLine("Poruka iz postupka Test");
    }
  }
}

