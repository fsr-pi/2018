using System;

namespace Postupci
{
  public class Program
  {
    public static void Main(string[] args)
    {      
      Postupak p = new Postupak();
      int a = 10, b = 7;
      int zbroj = p.Zbroji(a, b);
      int razlika = p.Oduzmi(a, b);

      //int brojOperacija = p.brOp; //ne možemo napisati  b.brOp jer je brOp privatna varijabla ('Postupci.Postupak.brOp' is inaccessible due to its protection level)

      //možemo je dohvatiti preko postupka
      int brojOperacija = p.GetBrOp();
      Console.WriteLine("{0} + {1} = {2}, {0} - {1} = {3}, Broj operacija = {4}",
        a, b, zbroj, razlika, brojOperacija);

      #region Ref i Out
      Console.WriteLine("Primjeri za ref i out");
      Postupak.SwapByVal(a, b);
      Console.WriteLine("Nakon SwapByVal a={0}  b={1}", a, b);

      Postupak.SwapByRef(ref a, ref b);
      Console.WriteLine("Nakon SwapByRef a={0}  b={1}", a, b);

      Postupak.TestVal(p);
      Console.WriteLine("Broj operacija u postupku p nakon TestVal = " + p.GetBrOp());

      Postupak.TestRef(ref p);
      Console.WriteLine("Broj operacija u postupku p nakon TestRef = " + p.GetBrOp());

      int i;   // varijabla ne mora biti inicijalizirana
      Postupak.TestOut(out i, 13);
      Console.WriteLine("Vrijednosti varijable i nakon TestOut =" + i);
      #endregion

      #region Primjeri s varijabilnim parametrima, pretpostavljenim vrijednostima i imenovanje parametara
      Console.WriteLine("\nIspis argumenata programa");
      Postupak.TestParams(args);  // postaviti argumente u razvojnom okruženju
      Postupak.TestParams("jedan", "dva", 3);

      Console.WriteLine("\nIspis prilikom poziva postupka bez parametara");
      Postupak.TestParams();

      Console.WriteLine("\nTestDefault za parametre 1,2, \"RPPP\"");
      Postupak.TestDefault(1, 2, "RPPP");
      Console.WriteLine("\nTestDefault za parametre 2, 15");
      Postupak.TestDefault(2, 15);
      Console.WriteLine("\nTestDefault za parametar 3");
      Postupak.TestDefault(3);
      Console.WriteLine("\nTestDefault za parametre 2, s: \"moj string\"");
      Postupak.TestDefault(2, s: "moj string");
      Console.WriteLine("\nTestDefault za parametre s: \"moj string\", b:55, a:4");
      Postupak.TestDefault(s: "moj string", b: 55, a: 4);
      #endregion

      #region Primjer sa svojstvom umjesto get postupka
      Console.WriteLine("\n Početni primjer, ali sa svojstvima umjesto get postupaka");
      Postupak_v2 p2 = new Postupak_v2();
      zbroj = p2.Zbroji(a, b);
      razlika = p2.Oduzmi(a, b);

      brojOperacija = p2.BrOp;
      Console.WriteLine("{0} + {1} = {2}, {0} - {1} = {3}, Broj operacija = {4}",
        a, b, zbroj, razlika, brojOperacija);

      //promijeni broj operacija - ako je postavljen i set dio svojstva
      //p3.BrOp = 100;
      #endregion
    }
  }
}
