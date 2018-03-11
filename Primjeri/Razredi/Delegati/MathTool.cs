using System;
using System.Collections.Generic;
using System.Text;

namespace Delegati
{
  public class MathTool
  {
    public static int sum(int x, int y)
    {
      return x + y;
    }

    public static int diff(int x, int y)
    {
      return x - y;
    }

    public static void printSquare(int x)
    {
      Console.WriteLine("x^2 = " + x * x);
    }

    public static void printSquareRoot(int x)
    {
      Console.WriteLine("sqrt(x) = " + Math.Sqrt(x));
    }
  }
}
