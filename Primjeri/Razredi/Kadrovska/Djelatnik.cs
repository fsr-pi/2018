// zamisljeni djelatnik
using System;

namespace Kadrovska
{
  public abstract class Djelatnik
  {
    protected string ime;
    protected string adresa;
    protected string telefon;

    public Djelatnik(string ime, string adresa, string telefon)
    {
      this.ime = ime;
      this.adresa = adresa;
      this.telefon = telefon;
    } // end of constructor

    public override string ToString()
    {
      string result = "ime: " + ime + "\n";

      result += "adresa: " + adresa + "\n";
      result += "telefon: " + telefon;

      return result;
    } // end of ToString

    // derivirani redefiniraju
    public abstract double Plati();

  } // end of class
} // end of namespace
