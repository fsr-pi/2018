using Firma.Mvc.Models;
using System.Collections.Generic;

namespace Firma.Mvc.ViewModels
{
  public class DrzaveViewModel
  {
    public IEnumerable<Drzava> Drzave { get; set; }
    public PagingInfo PagingInfo { get; set; }
  }
}
