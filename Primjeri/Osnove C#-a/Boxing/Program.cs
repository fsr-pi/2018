using System;

namespace Boxing
{
  public class Program
  {
    public static void Main(string[] args)
    {
      long l = 9600;
      object o = l; // o je objekt tipa System.Int64
      ShowObject(o);
      o = 4096; // o je objekt tipa System.Int32
      ShowObject(o);
      l = (long)(int)o; //l = (long)o je nepravilni cast. No, može l = Convert.ToInt64(o);

      Point point = new Point(42, 96);
      ShowObject(point);
      string text = "Tekst...";
      ShowObject(text);
    }

    static public void ShowObject(object o)
    {
      if (o is int)
        Console.WriteLine("The object is an integer");
      if (o is long)
        Console.WriteLine("The object is a long");
      if (o is Point)
        Console.WriteLine("The object is a Point structure");
      if (o is String)
        Console.WriteLine("The object is a String class object");
      Console.WriteLine("The value of object is " + o + "\r\n");
    }
  }
}
