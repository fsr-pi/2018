using System;

namespace Firma.Mvc.ViewModels
{
  public class PagingInfo
  {
    public int TotalItems { get; set; }
    public int ItemsPerPage { get; set; }
    public int CurrentPage { get; set; }
    public bool Ascending { get; set; }
    public int TotalPages
    {
      get
      {
        return (int)Math.Ceiling((decimal)TotalItems / ItemsPerPage);
      }
    }
    public int Sort { get; set; }
  }
}
