using System;
using System.Collections.Generic;

namespace Generics
{
  public class Program
  {
    static void Main(string[] args)
    {      
      PrimjerStog();
      PrimjerLista();
      PrimjerProsirenje();
    }

    private static void PrimjerProsirenje()
    {
      Dictionary<int, Stog<string>> dict = new Dictionary<int, Stog<string>>();
      Stog<string> stog = dict.DohvatiIliStvori(1);
      stog.Stavi("prvi");
      stog.Stavi("drugi");
      Console.WriteLine("ispis: " + stog);
      stog = dict.DohvatiIliStvori(1);
      stog.Stavi("treći");
      Console.WriteLine("ispis: " + stog);
      stog = dict.DohvatiIliStvori(2);
      stog.Stavi("četvrti");
      Console.WriteLine("ispis: " + stog);
    }

    private static void PrimjerStog()
    {
      // korištenje generičkog stoga tako da sprema cijele brojeve
      Stog<int> stogInt = new Stog<int>();
      stogInt.Stavi(5);
      stogInt.Stavi(7);
      stogInt.Stavi(9);

      Console.WriteLine("Skini sa stogInt = "
        + stogInt.Skini().ToString());

      // korištenje generičkog stoga tako da sprema stringove

      Stog<string> stogString = new Stog<string>();
      stogString.Stavi("jedan");
      stogString.Stavi("dva");

      Console.WriteLine("Skini sa stogString = "
        + stogString.Skini().ToString());

      
    }

    private static void PrimjerLista()
    {
      // korištenje gotovih generickih razreda (System.Collection.Generics)

      List<string> listaString = new List<string>();
      listaString.Add("jedan");
      listaString.Add("dva");
      listaString.Add("tri");

      string trazeni;

      do
      {
        Console.WriteLine("Traženi element liste: ");
        trazeni = Console.ReadLine();

        if (listaString.Contains(trazeni))
          Console.WriteLine("Element postoji u listi na "
            + listaString.IndexOf(trazeni) + ". mjestu.");
        else
          Console.WriteLine("Element ne postoji u listi!");

      } while (listaString.Contains(trazeni));
    }
  }
}
