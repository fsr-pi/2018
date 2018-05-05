using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Firma.Mvc.Models
{
    public class ViewDokumentInfo
    {
        public int IdDokumenta { get; set; }
        public decimal PostoPorez { get; set; }
        public int? IdPrethDokumenta { get; set; }
        public DateTime DatDokumenta { get; set; }
        public int IdPartnera { get; set; }
        public string NazPartnera { get; set; }
        public decimal IznosDokumenta { get; set; }
        public string VrDokumenta { get; set; }
        public int BrDokumenta { get; set; }

        [NotMapped]
        //Position in the result
        public int Position { get; set; }
    }
}
