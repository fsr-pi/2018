using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Firma.Mvc.Models
{
  public partial class Mjesto
  {
    public Mjesto()
    {
      PartnerIdMjestaIsporukeNavigation = new HashSet<Partner>();
      PartnerIdMjestaPartneraNavigation = new HashSet<Partner>();
    }

    public int IdMjesta { get; set; }

    [Required(ErrorMessage = "Potrebno je unijeti oznaku države")]
    [Display(Name = "Država")]
    public string OznDrzave { get; set; }
    [Required(ErrorMessage = "Potrebno je unijeti naziv mjesta")]
    [Display(Name = "Naziv mjesta")]
    public string NazMjesta { get; set; }
    [Required(ErrorMessage = "Potrebno je unijeti poštanski broj mjesta (10-60000)")]
    [Display(Name = "Poštanski broj mjesta")]
    [Range(10, 60000, ErrorMessage = "Dozvoljeni raspon: 10-60000")] //inače bi bilo 10000 do 60000, ali za potrebe testiranja, neka ide od 10
    public int PostBrMjesta { get; set; }
    [Display(Name = "Poštanski naziv mjesta")]
    public string PostNazMjesta { get; set; }

    public Drzava OznDrzaveNavigation { get; set; }
    public ICollection<Partner> PartnerIdMjestaIsporukeNavigation { get; set; }
    public ICollection<Partner> PartnerIdMjestaPartneraNavigation { get; set; }
  }
}

