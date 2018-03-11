using System;

namespace Konverzija
{
  public class Program
  {
    public static void Main(string[] args)
    {
      int a = 154;
      double b = a; //implicitna konverzija
      Console.WriteLine("int: {0}, double: {1}", a, b);

      string s = a.ToString();    //eksplicitna konverzija
      Console.WriteLine("int: {0}, string: {1}", a, s);

      int c = Int32.Parse(s);     //eksplicitna konverzija
      Console.WriteLine("int: {0}, int: {1}", a, c);

      decimal d = Convert.ToDecimal(c);   //eksplicitna konverzija
      Console.WriteLine("int: {0}, decimal: {1:N}", c, d);
    }
  }
}
