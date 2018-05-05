using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Firma.Mvc.ViewModels
{
    public class DokumentViewModel
    {
        public int IdDokumenta { get; set; }
        [Display(Name = "Vrsta dokumenta")]
        [Required(ErrorMessage = "Potrebno je odabrati vrstu dokumenta")]
        public string VrDokumenta { get; set; }

        [Required(ErrorMessage = "Potrebno je unijeti broj dokumenta")]
        [Display(Name = "Broj")]
        public int BrDokumenta { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy.}", ApplyFormatInEditMode = false)]
        [DataType(DataType.Date)]
        [Display(Name = "Datum")]
        [Required(ErrorMessage = "Potrebno je odabrati datum dokumenta")]
        public DateTime DatDokumenta { get; set; }

        [Display(Name = "Partner")]
        [Required(ErrorMessage = "Potrebno je odabrati poslovnog partnera")]
        public int? IdPartnera { get; set; }
        public string NazPartnera { get; set; }

        [Display(Name = "Prethodni dokument")]
        public int? IdPrethDokumenta { get; set; }
        public string NazPrethodnogDokumenta { get; set; }

        [Display(Name = "Porez (u %)")]
        [Required(ErrorMessage = "Potrebno je unijeti iznos poreza (u postotcima)")]
        [Range(0, 100, ErrorMessage = "Porez mora biti od 0 do 100")]        
        public int StopaPoreza { get; set; }

        public decimal PostoPorez
        {
            get
            {
                return StopaPoreza / 100m;
            }
            set
            {
                StopaPoreza = (int) (100m * value);
            }
        }

        [Display(Name = "Iznos")]
        public decimal IznosDokumenta { get; set; }
        public IEnumerable<StavkaViewModel> Stavke { get; set; }       

        public DokumentViewModel()
        {
            this.Stavke = new List<StavkaViewModel>();
        }
    }
}
