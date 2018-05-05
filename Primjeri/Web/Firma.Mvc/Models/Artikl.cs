using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Firma.Mvc.Models
{
  public partial class Artikl
  {
    public Artikl()
    {
      Stavka = new HashSet<Stavka>();
    }

    [Required(ErrorMessage = "Potrebno je unijeti šifru artikla")]
    [Display(Name = "Šifra artikla")]
    public int SifArtikla { get; set; }

    [Required(ErrorMessage = "Potrebno je unijeti naziv artikla")]
    [Display(Name = "Naziv artikla")]
    [MaxLength(255, ErrorMessage = "Naziv ne smije biti dulji od 255 znakova")]
    public string NazArtikla { get; set; }

    [Required(ErrorMessage = "Potrebno je unijeti jedinicu mjere")]
    [Display(Name = "Jedinica mjere")]
    [MaxLength(5, ErrorMessage = "Maksimalna duljina za jedinicu mjere je 5")]
    public string JedMjere { get; set; }

    [Required(ErrorMessage = "Potrebno je unijeti cijenu artikla")]
    [Display(Name = "Cijena artikla")]
    [Range(0.01, 100000.0, ErrorMessage = "Cijena mora biti pozitivni broj manji od 100 000")]
    public decimal CijArtikla { get; set; }

    [Display(Name = "Usluga")]
    public bool ZastUsluga { get; set; }

    [Display(Name = "Slika artikla")]
    public byte[] SlikaArtikla { get; set; }

    [Display(Name = "Opis artikla")]
    public string TekstArtikla { get; set; }

    public ICollection<Stavka> Stavka { get; set; }
  }
}
