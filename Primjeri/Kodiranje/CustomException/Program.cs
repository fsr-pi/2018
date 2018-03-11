using System;

namespace CustomException
{
  class Program
  {
    static void Main(string[] args)
    {
      int input = 0;
      int suma = 0;

      do // suma neparnih brojeva      
      {
        Console.Write("Neparan broj ili -1 za kraj: ");

        try
        {
          input = Int32.Parse(Console.ReadLine().ToString());
          if (input == -1) break; // da ne sumira oznaku kraja
          suma += Neparan(input);
        }
        catch (Iznimka e) //za ilustraciju vlastite iznimke, inače se ovaj primjer može puno bolje riješiti (provjera s if)
        {
          Console.WriteLine(e.Message);
        }
        catch (Exception e)
        {
          Console.WriteLine(e.Message);
        }
      } while (input != -1);

      Console.WriteLine("Suma=" + suma);
    }

    private static int Neparan(int val)
    {
      // bitovni AND - daje 1 ako je zadnji bit broja val = 1 tj. ako je val neparan broj 1 & 1 = 1  
      if ((val & 1) == 0)
      {
        //throw new Iznimka();
        throw new Iznimka(val.ToString());
      }
      return val;
    }

  }
}