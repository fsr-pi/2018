using System;

namespace CheckedUnchecked
{
  class Program
  {

    static void Main(string[] args)
    {
      int i, deset = 10;

      // bez provjere preljeva
      i = unchecked(2147483647 + deset);
      Console.WriteLine(i);

      try
      {
        // provjera preljeva
        i = checked(2147483647 + deset);
        Console.WriteLine(i);
      }
      catch (Exception e)
      {
        Console.WriteLine("Exception: {0}", e.Message);
      }
    }
  }

  // primjer dinamičke prilagodbe
  class Counter
  {
    public int Value = 0;
    public bool EnableCheck = false; // zastavica provjere

    public Counter(int val, bool enableCheck)
    {
      this.Value = val;
      this.EnableCheck = enableCheck;
    }

    public static Counter operator +(Counter op1, int op2)
    {
      int newVal;

      if (op1.EnableCheck)
        // množenje uz provjeru
        newVal = checked(op1.Value + op2);

      else
        // množenje bez provjere
        newVal = unchecked(op1.Value + op2);

      return new Counter(newVal, op1.EnableCheck);
    }
  }
}

