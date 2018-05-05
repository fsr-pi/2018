using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Firma.Mvc.Models
{
  public class StavkaDenorm
  {
    public string OIB { get; set; }
    public string NazPartnera { get; set; }
    public int IdDokumenta { get; set; }
    public DateTime DatDokumenta { get; set; }
    public decimal IznosDokumenta { get; set; }
    public int IdStavke { get; set; }
    public int SifArtikla { get; set; }
    public decimal KolArtikla { get; set; }
    public decimal JedCijArtikla { get; set; }
    public decimal PostoRabat { get; set; }
    public string NazArtikla { get; set; }
    [NotMapped]
    public string UrlDokumenta { get; set; }
  }
}
