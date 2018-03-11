using System;
interface IPoint
{
  // Property signatures:
  int X { get; set; }
  int Y { get; set; }
}

class MyPoint : IPoint
{
  public int X { get; set; }
  public int Y { get; set; }

  // Constructor:   
  public MyPoint(int x, int y)
  {
    X = x;
    Y = y;
  }  
}

class InterfaceMain
{
  private static void PrintPoint(IPoint p)
  {
    Console.WriteLine(
      "MyPoint: x={0}, y={1}",
      p.X, p.Y);
  }

  public static void Main()
  {
    MyPoint p = new MyPoint(2, 3);
    PrintPoint(p);
  }
}

