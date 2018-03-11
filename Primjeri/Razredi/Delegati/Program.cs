using System;

namespace Delegati
{
  class Program
  {
    public delegate int MathFunction(int a, int b);
    public delegate void PrintFunction(int n);
    static void Main(string[] args)
    {
      int x = 16, y = 2;
      MathFunction mf = MathTool.sum;
      Console.WriteLine("mf({0}, {1}) = {2}", x, y, mf(x, y));
      mf = MathTool.diff;
      Console.WriteLine("mf({0}, {1}) = {2}", x, y, mf(x, y));
      PrintFunction pf = MathTool.printSquare;
      pf += MathTool.printSquareRoot;
      pf(x);
      pf -= MathTool.printSquare;
      Console.WriteLine();
      pf(y);
      Func<int, int, int> func = MathTool.sum;
      Console.WriteLine("func({0}, {1}) = {2}", x, y, func(x, y));
      Action<int> action = MathTool.printSquare;
      action(x);
    }
  }
}