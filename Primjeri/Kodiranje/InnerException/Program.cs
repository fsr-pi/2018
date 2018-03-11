using System;

namespace InnerException
{
  class Primjer
  {
    public void F()
    {
      try
      {
        int x = 0;
        int y = 10 / x;
      }
      catch (Exception e)
      {
        throw new Exception("Iznimka u Primjer.F() :", e);
      }
    }

  }

  class Program
  {
    static void Main(string[] args)
    {
      Primjer p = new Primjer();
      try
      {
        p.F();
      }
      catch (Exception e)
      {
        Console.WriteLine("Iznimka u Main: {0}\nInnerException: {1}",
           e.Message, e.InnerException.Message);
      }

    }
  }
}