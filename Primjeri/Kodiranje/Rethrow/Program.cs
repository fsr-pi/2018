using System;

namespace Rethrow
{
  class Program
  {
    static void Main(string[] args)
    {
      try
      {
        RethrowException();
      }
      catch (Exception e)
      {
        Console.WriteLine($"Main: {e.Message}\n{e.StackTrace}");
      }

    }

    static void MetodaKojaBacaIznimku()
    {
      // poziv bilo koje naredbe ili postupka koji izaziva iznimku
      throw new Exception("Iznimka je bacena!");
    }

    static void RethrowException()
    {
      try
      {
        MetodaKojaBacaIznimku();
      }
      catch (Exception e)
      {
        Console.WriteLine($"Rethrow: {e.Message}\n{e.StackTrace}");
        throw; //isprobati što je e.StackTrace u hvatanju ove iznimke, ako je bilo throw ili throw e;
      }
      finally
      {
        Console.WriteLine("Finally RethrowException");
      }
    }
  }
}