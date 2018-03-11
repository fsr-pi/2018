// taxist
using System;

namespace Kadrovska
{
  public class PoSatu : Zaposlenik
  {
    private int satiRada;

    public PoSatu(string ime, string adresa, string telefon,
                  string radnaKnj, double placa)
      : base(ime, adresa, telefon, radnaKnj, placa)
    {
      satiRada = 0;
      // placa ovdje ima smisao cijene sata
      // iznos isplate bude po uèinku
    }

    public void PisiSate(int josSati)
    {
      satiRada += josSati;
    }

    public override double Plati()
    {
      double isplata = placa * satiRada;

      satiRada = 0; // da ne bi 2 puta

      return isplata;
    }

    public override string ToString()
    {
      string result = base.ToString();

      result += "\nUkupno sati: " + satiRada;

      return result;
    } // end of method ToString

  } // end of class
} //end of namespace