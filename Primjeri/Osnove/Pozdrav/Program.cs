using System;
using System.Text;

namespace Pozdrav
{
  public class Program
  {
    public static void Main(string[] args)
    {
      Console.OutputEncoding = Encoding.UTF8;      

      Console.WriteLine("Pozdrav! Unesite tekst");
     
      string line = Console.ReadLine();
      Console.WriteLine("Upisani tekst je " + line);
      Console.WriteLine("Upisani tekst je {0}",  line);
      Console.WriteLine($"Upisani tekst je {line}");
    }
  }
}
