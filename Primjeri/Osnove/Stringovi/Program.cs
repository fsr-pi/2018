using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stringovi
{
  public class Program
  {
    public static void Main(string[] args)
    {
      Console.OutputEncoding = Encoding.UTF8;
      string ss = string.Empty;

      Console.WriteLine("\n## String");
      string a = "abcd"; //System.String a
      Console.WriteLine("Duljina {0} je {1}.", a, a.Length);

      string b = a.Replace('b', 'o');
      Console.WriteLine("Zamjenom 'b' s 'o':\n{0}", b);

      string c = b.Insert(3, "xy");
      string d = c.ToUpper();
      Console.WriteLine("Umetanjem 'xy' na 3.mjesto i ToUpper:\n{0}", d);

      string e = d.Remove(0, 3);
      Console.WriteLine("Brisanjem prva 3 znaka:\n{0}", e);

      Console.WriteLine("\nFormatiranje stringa: ");
      Console.WriteLine("Desno poravnati, širine 5:\n{0,5} {1,5}", 123, 456);
      Console.WriteLine("Lijevo poravnati, širine 5:\n{0,-5} {1,-5}", 123, 456);

      Console.WriteLine("  Obrubljen string                  ".Trim() + "#");
      Console.WriteLine("Prazan string \"" + string.Empty + "\"");

      string text = "Pero Perić 23\nMarija Marić 20\nIvo Ivić 21";
      string[] niz = text.Split('\n');
      Console.WriteLine(niz); // za ispis vrijednosti koristiti petlju

      //System.Text.StringBuilder

      Console.WriteLine("\n## StringBuilder");
      StringBuilder sb = new StringBuilder();

      foreach (string str in niz)
      {
        string[] s = str.Split(' ');

        sb.Append(s[0]);
        sb.Append(" ");
        sb.Append(s[1].ToUpper());
        sb.Append(" ");
        sb.Append(s[2]);
        sb.Append("\n");
      }

      //ToString()

      Console.WriteLine(sb.ToString());

      int x = 2;
      Console.WriteLine("a" + x); //automatski poziv metode ToString() na broju x

      int[] nizbrojeva = { 1, 2, 3, 4, 5, 6, 7 };
      ss = string.Join(",", nizbrojeva);
      Console.WriteLine(ss);

      ss = string.Join("--", "Spoji", "u", 1, "string");
      Console.WriteLine(ss);
    }
  }
}