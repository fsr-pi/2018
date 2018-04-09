using System;
using System.Collections.Generic;

namespace Firma.Mvc.Models
{
    public partial class Partner
    {
        public Partner()
        {
            Dokument = new HashSet<Dokument>();
        }

        public int IdPartnera { get; set; }
        public string TipPartnera { get; set; }
        public string Oib { get; set; }
        public int? IdMjestaPartnera { get; set; }
        public string AdrPartnera { get; set; }
        public int? IdMjestaIsporuke { get; set; }
        public string AdrIsporuke { get; set; }

        public Mjesto IdMjestaIsporukeNavigation { get; set; }
        public Mjesto IdMjestaPartneraNavigation { get; set; }
        public Osoba Osoba { get; set; }
        public Tvrtka Tvrtka { get; set; }
        public ICollection<Dokument> Dokument { get; set; }
    }
}
