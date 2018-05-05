using System.Collections.Generic;

namespace Firma.Mvc.ViewModels
{
  public class ArtikliViewModel
  {
    public IEnumerable<ArtiklViewModel> Artikli { get; set; }
    public PagingInfo PagingInfo { get; set; }
  }
}
