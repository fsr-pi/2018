using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;


class Osoba
{
  PoslovniPartner poslovniPartner;

  public string Ime { get; set; }
  public string Prezime { get; set; }

  public Osoba(string maticniBroj, string adresaSjedista, string adresaIsporuke,
    string ime, string prezime)
  {
    this.Ime = ime;
    this.Prezime = prezime;
    this.poslovniPartner = new PoslovniPartner(maticniBroj, adresaSjedista, adresaIsporuke);
  }

  public override string ToString()
  {
    return Ime + " " + Prezime + "\n" + poslovniPartner.ToString();
  }

}
