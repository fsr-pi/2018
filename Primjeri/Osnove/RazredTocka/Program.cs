using System;

namespace RazredTocka
{
  public class Program
  {
    public static void Main(string[] args)
    {      
      Tocka t = new Tocka(1, -2);
      Console.WriteLine("Točka ({0},{1}) je u {2}.kvadrantu.", t.x, t.y, t.Kvadrant());
    }
  }
}
