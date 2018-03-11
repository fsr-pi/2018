using System;

namespace VrijednostiReference
{
  class Razred
  {
    public int Value = 0;
  }
  class Program
  {
    static void Main()
    {
      int val1 = 0;
      int val2 = val1;
      val2 = 5;
      Razred ref1 = new Razred();
      Razred ref2 = ref1;
      ref2.Value = 123;
      Console.WriteLine("V: {0}, {1}", val1, val2);
      Console.WriteLine("R: {0}, {1}", ref1.Value, ref2.Value);
    }
  } 
}
