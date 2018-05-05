namespace Firma.Mvc.ViewModels
{
  public class ArtiklViewModel
  {
    public int SifraArtikla { get; set; }
    public string NazivArtikla { get; set; }
    public string JedinicaMjere { get; set; }
    public decimal CijenaArtikla { get; set; }
    public bool Usluga { get; set; }
    public string TekstArtikla { get; set; }
    public bool ImaSliku { get; set; }
    public int ImageHash { get; set; }
  }
}
