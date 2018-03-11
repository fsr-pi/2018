using System;
using System.Collections.Generic;
using System.Text;


class PoslovniPartner
{

  public string MaticniBroj { get; private set; }
  public string AdresaSjedista { get; set; }
  public string AdresaIsporuke { get; set; }

  public PoslovniPartner(string MaticniBroj, string AdresaSjedista, string AdresaIsporuke)
  {
    this.MaticniBroj = MaticniBroj;
    this.AdresaSjedista = AdresaSjedista;
    this.AdresaIsporuke = AdresaIsporuke;
  }

  //Override metode ToString() koja je nasljeðena iz razreda System.Object
  public override string ToString() //Ovu metodu nije potrebno implementirati u razredu koji nasljeðuje ovaj! Ukoliko je implementriamo potrebno je dodati kljuènu rijeè override!
  {
    return MaticniBroj +
      "\nSjedište: " + AdresaSjedista +
      "\nIsporuka: " + AdresaIsporuke;
  }

}
