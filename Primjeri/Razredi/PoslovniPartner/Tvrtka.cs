namespace PoslovniPartner
{
  class Tvrtka : PoslovniPartner
  {
    public string Naziv { get; private set; }

    public Tvrtka(string maticniBroj, string adresaSjedista, string adresaIsporuke,
      string naziv)
      : base(maticniBroj, adresaSjedista, adresaIsporuke)
    {
      this.Naziv = naziv;
    }

    //Override pišemo uvijek kada implementiramo abstract metode razreda kojeg nasljeðujemo
    public override bool ValidacijaMaticnogBroja()
    {
      if (MaticniBroj.Length == 7) return true;
      else return false;
    }

    public override string ToString()
    {
      return Naziv + "\n" + base.ToString();
    }

  }
}