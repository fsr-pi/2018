using System;
using System.Collections.Generic;

namespace Firma.Mvc.Models
{
    public partial class Tvrtka
    {
        public int IdTvrtke { get; set; }
        public string MatBrTvrtke { get; set; }
        public string NazivTvrtke { get; set; }

        public Partner IdTvrtkeNavigation { get; set; }
    }
}
