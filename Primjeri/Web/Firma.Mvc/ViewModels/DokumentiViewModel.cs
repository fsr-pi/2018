using Firma.Mvc.Models;
using System.Collections.Generic;

namespace Firma.Mvc.ViewModels
{
    public class DokumentiViewModel
    {
        public IEnumerable<ViewDokumentInfo> Dokumenti { get; set; }
        public PagingInfo PagingInfo { get; set; }
        public DokumentFilter Filter { get; set; }
    }
}
