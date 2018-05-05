using System.ComponentModel.DataAnnotations;

namespace Firma.Mvc.ViewModels
{
    public class StavkaViewModel
    {
        public int IdStavke { get; set; }      
        public int SifArtikla { get; set; }
        public string NazArtikla { get; set; }
        public decimal KolArtikla { get; set; }
        public decimal JedCijArtikla { get; set; }
        public decimal PostoRabat { get; set; }

        public decimal IznosArtikla
        {
            get
            {
                return KolArtikla * JedCijArtikla * (1 - PostoRabat);
            }
        }
    }
}
