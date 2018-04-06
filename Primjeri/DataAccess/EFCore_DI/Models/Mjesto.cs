using System;
using System.Collections.Generic;

namespace EFCore_DI.Models
{
    public partial class Mjesto
    {
        public Mjesto()
        {
            PartnerIdMjestaIsporukeNavigation = new HashSet<Partner>();
            PartnerIdMjestaPartneraNavigation = new HashSet<Partner>();
        }

        public int IdMjesta { get; set; }
        public string OznDrzave { get; set; }
        public string NazMjesta { get; set; }
        public int PostBrMjesta { get; set; }
        public string PostNazMjesta { get; set; }

        public virtual ICollection<Partner> PartnerIdMjestaIsporukeNavigation { get; set; }
        public virtual ICollection<Partner> PartnerIdMjestaPartneraNavigation { get; set; }
        public virtual Drzava OznDrzaveNavigation { get; set; }
    }
}
