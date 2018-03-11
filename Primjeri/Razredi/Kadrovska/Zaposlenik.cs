// placenik
using System;

namespace Kadrovska
{
  public class Zaposlenik : Djelatnik
  {
    protected string radnaKnj;
    protected double placa; // mjesecna

    public Zaposlenik(string ime, string adresa, string telefon,
               string radnaKnj, double placa)
      : base(ime, adresa, telefon)
    {
      this.radnaKnj = radnaKnj;
      this.placa = placa;
    }

    public override string ToString()
    {
      string result = base.ToString();

      result += "\nBroj radne knjige: " + radnaKnj;

      return result;
    }

    public override double Plati()
    {
      return placa;
    }
  } // end of class
} // end of namespace
