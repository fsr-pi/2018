using Firma.Mvc.Models;
using System.Collections.Generic;

namespace Firma.Mvc.ViewModels
{
    public class PartneriViewModel
    {
        public IEnumerable<ViewPartner> Partneri { get; set; }
        public PagingInfo PagingInfo { get; set; }       
    }
}
