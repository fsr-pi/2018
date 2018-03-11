using System;


namespace Postupci
{
    public class Postupak
    {
    // učahurivanje (encapsulation) - skrivanje člana
    // brojač pozvanih operacija (zbroja i razlike)
    private int brOp = 0;

    public int Zbroji(int x, int y)
    {
      ++brOp;
      return x + y;
    }

    public int Oduzmi(int x, int y)
    {
      ++brOp;
      return x - y;
    }

    // pristup skrivenom članu može se obaviti javnim postupcima
    public int GetBrOp() { return brOp; }




    public static void SwapByVal(int a, int b)
    {
      int temp = a;
      a = b;
      b = temp;
    }
    public static void SwapByRef(ref int a, ref int b)
    {
      int temp = a;
      a = b;
      b = temp;
    }

    public static void TestVal(Postupak post)
    {
      post = new Postupak();
    }

    public static void TestRef(ref Postupak post)
    {
      post = new Postupak();
    }

    public static void TestOut(out int val, int defval)
    {
      val = defval; // izlazni argument mora biti postavljen
    }

    public static void TestParams(params object[] args)
    {
      Console.WriteLine("TestParams ispis parametara s for petljom: ");
      for (int i = 0; i < args.Length; i++)
        Console.WriteLine("{0}:{1}", i, args[i]);


      // alternativa s foreach
      Console.WriteLine("TestParams ispis parametara (i njihovih tipova) s foreach petljom: ");
      foreach (object o in args)
        Console.WriteLine("{0}:{1}", o.GetType().ToString(), o);

      Console.WriteLine();
    }

    public static void TestDefault(int a, int b = 99999, string s = "default string")
    {
      Console.WriteLine("Ispis u TestDefault a = {0}, b = {1}, s = {2}", a, b, s);
    }
  }
}
