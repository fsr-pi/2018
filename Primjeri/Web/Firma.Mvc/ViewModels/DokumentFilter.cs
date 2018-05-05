using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using Firma.Mvc.Models;

namespace Firma.Mvc.ViewModels
{
  public class DokumentFilter : IPageFilter
  {
    public int? IdPartnera { get; set; }
    public string NazPartnera { get; set; }
    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy.}", ApplyFormatInEditMode = false)]
    public DateTime? DatumOd { get; set; }
    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy.}", ApplyFormatInEditMode = false)]
    public DateTime? DatumDo { get; set; }
    public decimal? IznosOd { get; set; }
    public decimal? IznosDo { get; set; }

    public bool IsEmpty()
    {
      bool active = IdPartnera.HasValue
                    || DatumOd.HasValue
                    || DatumDo.HasValue
                    || IznosOd.HasValue
                    || IznosDo.HasValue;
      return !active;
    }

    public override string ToString()
    {
      return string.Format("{0}-{1}-{2}-{3}-{4}",
          IdPartnera,
          DatumOd?.ToString("dd.MM.yyyy"),
          DatumDo?.ToString("dd.MM.yyyy"),
          IznosOd,
          IznosDo);
    }

    public static DokumentFilter FromString(string s)
    {
      var filter = new DokumentFilter();
      var arr = s.Split(new char[] { '-' }, StringSplitOptions.None);
      try
      {
        filter.IdPartnera = string.IsNullOrWhiteSpace(arr[0]) ? new int?() : int.Parse(arr[0]);
        filter.DatumOd = string.IsNullOrWhiteSpace(arr[1]) ? new DateTime?() : DateTime.ParseExact(arr[1], "dd.MM.yyyy", CultureInfo.InvariantCulture);
        filter.DatumDo = string.IsNullOrWhiteSpace(arr[2]) ? new DateTime?() : DateTime.ParseExact(arr[2], "dd.MM.yyyy", CultureInfo.InvariantCulture);
        filter.IznosOd = string.IsNullOrWhiteSpace(arr[3]) ? new decimal?() : decimal.Parse(arr[3]);
        filter.IznosDo = string.IsNullOrWhiteSpace(arr[4]) ? new decimal?() : decimal.Parse(arr[4]);
      }
      catch { } //to do: log...
      return filter;
    }

    public IQueryable<ViewDokumentInfo> Apply(IQueryable<ViewDokumentInfo> query)
    {
      if (IdPartnera.HasValue)
      {
        query = query.Where(d => d.IdPartnera == IdPartnera.Value);
      }
      if (DatumOd.HasValue)
      {
        query = query.Where(d => d.DatDokumenta >= DatumOd.Value);
      }
      if (DatumDo.HasValue)
      {
        query = query.Where(d => d.DatDokumenta <= DatumDo.Value);
      }
      if (IznosOd.HasValue)
      {
        query = query.Where(d => d.IznosDokumenta >= IznosOd.Value);
      }
      if (IznosDo.HasValue)
      {
        query = query.Where(d => d.IznosDokumenta <= IznosDo.Value);
      }
      return query;
    }   
  }
}
