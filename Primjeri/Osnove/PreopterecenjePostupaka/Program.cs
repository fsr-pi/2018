using System;

namespace PreopterecenjePostupaka
{
  public class Program
  {
    static void Main(string[] args)
    {
      // koji Maximum (double ili int) ?
      Console.WriteLine("maximum1: " + Maximum(1, 3, 2));
      Console.WriteLine("maximum2: " + Maximum(1.0, 3, 2));
    }

    static double Maximum(double x, double y, double z)
    {
      Console.WriteLine("double Maximum( double x, double y, double z )");
      return Math.Max(x, Math.Max(y, z));
    }

    static int Maximum(int x, int y, int z)
    {
      Console.WriteLine("int Maximum( int x, int y, int z )");
      return Math.Max(x, Math.Max(y, z));
    }
  }
}
