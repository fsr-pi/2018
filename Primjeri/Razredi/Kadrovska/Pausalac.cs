// menadzerski ugovornik
using System;

namespace Kadrovska
{
  public class Pausalac : Zaposlenik
  {
    private double bonus;

    public Pausalac(string ime, string adresa, string telefon,
                    string radnaKnj, double placa)
      : base(ime, adresa, telefon, radnaKnj, placa)
    {
      // placa je placa po ugovoru
      bonus = 0;  // bonus nije zasluzio
    }

    public void Dividendaj(double dividenda)
    {
      bonus = dividenda;
    }

    public override double Plati()
    {
      double isplata = base.Plati() + bonus;

      bonus = 0; // da ne bi jos

      return isplata;
    } // end of Plati

  } // end of class

} // end of namespace
