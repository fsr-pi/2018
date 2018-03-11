using System;

namespace Razred
{
  public class Program
  {
    public static void Main(string[] args)
    {
      // testirati uvidom u Autos i Locals
      const int D = Razred.konst + 1; // deklaracija konstante u metodi, konstantom razreda

      Razred.trajna += 1; // promjena na razini razreda
                          //Razred.var +=1; // promjena moguća samo na razini objekta, a ne razreda

      Razred r1 = new Razred();
      r1.var += 10; // promjena na razini objekta
                    // koje su vrijednosti članova r1 ?

      Razred r2 = new Razred(13, 666);
      // koje su vrijednosti članova r2 ?

      //r2.neDiraj +=1; // nije dozvoljeno

      Razred r3 = r2; // referenca
      r3.var = 99;
      // koje su vrijednosti članova r3 i r2 ?

      //pridruživanje vrijednosti javnim članovima prilikom stvaranja objekta
      Razred r4 = new Razred() { var = 5 }; //može i Razred r4 = new Razred { var = 5 };

      int zbroj = r4.Zbroji(2, 5);
      int razlika = Razred.Oduzmi(6, 3); //statički postupak

      Console.WriteLine("Pozivam garbage collector - enter za nastavak");
      GC.Collect();
      GC.WaitForPendingFinalizers();
      Console.ReadLine();

      Console.WriteLine("Pozivam garbage collector - enter za nastavak");
      r1 = null;
      GC.Collect();
      GC.WaitForPendingFinalizers();
      Console.ReadLine();

      Console.WriteLine("Kraj programa.");          
    }
  }
}
