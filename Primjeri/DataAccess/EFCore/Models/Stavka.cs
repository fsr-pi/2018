using System;
using System.Collections.Generic;

namespace EFCore.Models
{
    public partial class Stavka
    {
        public int IdStavke { get; set; }
        public int IdDokumenta { get; set; }
        public int SifArtikla { get; set; }
        public decimal KolArtikla { get; set; }
        public decimal JedCijArtikla { get; set; }
        public decimal PostoRabat { get; set; }

        public virtual Dokument IdDokumentaNavigation { get; set; }
        public virtual Artikl SifArtiklaNavigation { get; set; }
    }
}
