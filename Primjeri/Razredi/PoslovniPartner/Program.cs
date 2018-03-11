using System;

namespace PoslovniPartner
{
  class Program
  {
    static void Main(string[] args)
    {           
      try
      {
        // polje poslovnih partnera, tj. onih opcenito
        PoslovniPartner[] partneri = new PoslovniPartner[2];

        // Za vježbu prevesti s MaticniBroj veći/manji od 13, odnosno 7 znamenki...

        // evidencija specijaliziranih partnera, tj. izvedenih 
        partneri[0] = new Osoba("1234567891123",
          "Adresa doma", "Adresa na poslu",
          "Jakov", "Fizikalac");
        partneri[1] = new Tvrtka("1234567",
          "Unska 3", "Vukovarska 39", "FER");

        // petlja po poslovnim partnerima uz poziv nadjačane metode
        foreach (PoslovniPartner p in partneri)
        {
          Console.WriteLine("------------------------");
          Console.WriteLine(p.ToString());
        }
      }
      catch (Exception e)
      {
        Console.WriteLine(e.Message);
      }

    }
  }
}