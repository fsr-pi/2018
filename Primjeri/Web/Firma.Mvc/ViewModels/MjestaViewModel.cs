using System.Collections.Generic;

namespace Firma.Mvc.ViewModels
{
  public class MjestaViewModel
  {
    public IEnumerable<MjestoViewModel> Mjesta { get; set; }
    public PagingInfo PagingInfo { get; set; }
  }
}
