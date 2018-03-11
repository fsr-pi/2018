using System;
using System.Text;

namespace Pozdrav
{
  public class Program
  {
    public static void Main(string[] args)
    {
      //.NET Core inicijalno podržava manji broj kodnih stranica, a input na Windowsima
      //s HR tipkovnicom je u kodnoj stranici 1250
      Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
      Console.OutputEncoding = Encoding.UTF8;
      Console.InputEncoding = Encoding.GetEncoding(1250) ;

      Console.WriteLine("Pozdrav: ");

      string line = Console.ReadLine();
      Console.WriteLine("Upisani tekst je " + line);
      Console.WriteLine("Upisani tekst je {0}", line);
      Console.WriteLine($"Upisani tekst je {line}");
    }
  }
}
