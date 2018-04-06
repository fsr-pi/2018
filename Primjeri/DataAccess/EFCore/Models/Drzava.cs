using System;
using System.Collections.Generic;

namespace EFCore.Models
{
    public partial class Drzava
    {
        public Drzava()
        {
            Mjesto = new HashSet<Mjesto>();
        }

        public string OznDrzave { get; set; }
        public string NazDrzave { get; set; }
        public string Iso3drzave { get; set; }
        public int? SifDrzave { get; set; }

        public virtual ICollection<Mjesto> Mjesto { get; set; }
    }
}
