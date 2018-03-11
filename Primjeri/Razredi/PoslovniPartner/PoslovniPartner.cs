using System;

namespace PoslovniPartner
{
  abstract class PoslovniPartner
  {
    public string MaticniBroj { get; private set; }
    public string AdresaSjedista { get; set; }
    public string AdresaIsporuke { get; set; }


    public PoslovniPartner(string maticniBroj, string adresaSjedista, string adresaIsporuke)
    {
      this.MaticniBroj = maticniBroj;
      // obavlja se validacija preoptereæenom metodom!
      if (!this.ValidacijaMaticnogBroja())
        throw new Exception("Pogreška unosa matiènog broja!");
      this.AdresaSjedista = adresaSjedista;
      this.AdresaIsporuke = adresaIsporuke;
    }

    //Override metode ToString() koja je nasljeðena iz razreda System.Object
    //Ovu metodu nije potrebno implementirati u razredu koji nasljeðuje ovaj! 
    //Ukoliko je implementriamo potrebno je dodati kljuènu rijeè override!
    public override string ToString()
    {
      return MaticniBroj +
        "\nAdresa Sjedišta: " + AdresaSjedista +
        "\nAdresa Isporuke: " + AdresaIsporuke;
    }

    public abstract bool ValidacijaMaticnogBroja(); //Zbog abstract,
                                                    // ovu metodu potrebno je implementirati u razredu koji nasljeðuje ovaj! 
                                                    // Takoðer, abstract znaèi da je neæemo implementirati ovdje!

  }
}