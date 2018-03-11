// entuzijast
using System;

namespace Kadrovska
{
  public class Honorarac : Djelatnik // u našem sluèaju volonter
  {
    public Honorarac(string ime, string adresa, string telefon)
      : base(ime, adresa, telefon)
    {
    }

    public override double Plati()
    {
      return 0.0;
    } // end of method Plati
  } // end of class
} // end of namespace
