using System.ComponentModel.DataAnnotations;

namespace Firma.Mvc.ViewModels
{
    public class PartnerViewModel
    {       
        public int IdPartnera { get; set; }
        [RegularExpression("[OT]")]
        public string TipPartnera { get; set; }
        [Display(Name = "Prezime")]
        public string PrezimeOsobe { get; set; }
        [Display(Name = "Ime")]
        public string ImeOsobe { get; set; }
        [Display(Name = "Matični broj tvrtke")]
        public string MatBrTvrtke { get; set; }
        [Display(Name = "Naziv")]
        public string NazivTvrtke { get; set; }
        [Required]        
        [RegularExpression("[0-9]{11}")]
        [Display(Name = "OIB")]
        public string Oib { get; set; }
        [Display(Name = "Adresa")]
        public string AdrPartnera { get; set; }
        [Display(Name = "Mjesto")]
        public int? IdMjestaPartnera { get; set; }
        public string NazMjestaPartnera { get; set; } 
        [Display(Name="Adresa isporuke")]       
        public string AdrIsporuke { get; set; }
        [Display(Name = "Mjesto isporuke")]
        public int? IdMjestaIsporuke { get; set; }
        public string NazMjestaIsporuke { get; set; }       
    }
}
