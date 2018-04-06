using System;
using System.Collections.Generic;

namespace EFCore_DI.Models
{
    public partial class Artikl
    {
        public Artikl()
        {
            Stavka = new HashSet<Stavka>();
        }

        public int SifArtikla { get; set; }
        public string NazArtikla { get; set; }
        public string JedMjere { get; set; }
        public decimal CijArtikla { get; set; }
        public bool ZastUsluga { get; set; }
        public byte[] SlikaArtikla { get; set; }
        public string TekstArtikla { get; set; }

        public virtual ICollection<Stavka> Stavka { get; set; }
    }
}
