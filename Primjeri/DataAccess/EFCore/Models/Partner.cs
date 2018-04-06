using System;
using System.Collections.Generic;

namespace EFCore.Models
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

        public virtual ICollection<Dokument> Dokument { get; set; }
        public virtual Osoba Osoba { get; set; }
        public virtual Tvrtka Tvrtka { get; set; }
        public virtual Mjesto IdMjestaIsporukeNavigation { get; set; }
        public virtual Mjesto IdMjestaPartneraNavigation { get; set; }
    }
}
