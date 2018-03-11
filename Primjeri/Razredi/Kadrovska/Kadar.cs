// kadar
using System;

namespace Kadrovska
{
  public class Kadar
  {
    private Djelatnik[] lista;

    public Kadar()
    {
      lista = new Djelatnik[6];

      lista[0] = new Pausalac("Bobi", "Bobinje bb",
        "555-0469", "123-45-6789", 2423.07);

      lista[1] = new Zaposlenik("Rudi", "Rudinje 1",
        "555-0101", "987-65-4321", 1246.15);

      lista[2] = new Zaposlenik("Ivica", "Ivanecka 2",
        "555-0000", "010-20-3040", 1169.23);

      lista[3] = new PoSatu("Janica", "Janicka 3",
        "555-0690", "958-47-3625", 10.55);

      lista[4] = new Honorarac("Bakica", "Bakutanerska 4",
        "555-8374");

      lista[5] = new Honorarac("Schwortz", "Besplatna 5",
        "555-7282");

      // dodaj bonus, zahtijeva cast
      ((Pausalac)lista[0]).Dividendaj(500.00);

      // dodaj sate, zahtijeva cast
      ((PoSatu)lista[3]).PisiSate(40);
    }

    public void Isplata()
    {
      double iznos;

      for (int count = 0; count < lista.Length; count++)
      {
        Console.WriteLine(lista[count]);

        iznos = lista[count].Plati();  // polymorphic

        if (iznos == 0.0)
          Console.WriteLine("Hvala na trudu!");
        else
          Console.WriteLine("Izvolite: {0:C}", iznos);

        Console.WriteLine("-----------------------------------");

      } // end of for loop

    } // end of method Isplata

  } // end of class

} // end of namespace
