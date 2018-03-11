using System;

namespace TryCatch
{
  class Program
  {
    static void Main(string[] args)
    {
      try
      {
        int x = new Random().Next(2); //0 ili 1
        int y = 10 / x;

        int[] a = { 1, 2, 3 };
        Console.WriteLine(a[3].ToString());
      }
      catch (DivideByZeroException e)
      {
        Console.WriteLine("Dijeljenje s 0. " + e.Message);
      }
      catch (IndexOutOfRangeException e)
      {
        Console.WriteLine("Indeks izvan granica polja. " + e.Message);
      }
      catch (Exception e)
      {
        Console.WriteLine("Pogreska. " + e.Message);
      }
      finally
      {
        Console.WriteLine("Napokon.\n");
      }
    }
  }
}