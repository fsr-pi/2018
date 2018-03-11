namespace PoslovniPartner
{
  class Osoba : PoslovniPartner
  {
    public string Ime { get; private set; }

    public string Prezime { get; set; }

    public Osoba(string maticniBroj, string adresaSjedista, string adresaIsporuke,
      string ime, string prezime)
      : base(maticniBroj, adresaSjedista, adresaIsporuke)
    {
      this.Ime = ime;
      this.Prezime = prezime;
    }

    public override string ToString()
    {
      return Ime + " " + Prezime + "\n" + base.ToString();
    }

    public override bool ValidacijaMaticnogBroja()
    {
      if (MaticniBroj.Length == 13) return true;
      else return false;
    }
  }
}