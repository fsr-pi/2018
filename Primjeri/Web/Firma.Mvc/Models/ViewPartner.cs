namespace Firma.Mvc.Models
{
    public class ViewPartner
    {
        public int IdPartnera { get; set; }
        public string TipPartnera { get; set; }
        public string OIB { get; set; }
        public string Naziv { get; set; }  
        public string TipPartneraText
        {
            get
            {
                if (TipPartnera == "O")
                {
                    return "Osoba";
                }
                else
                {
                    return "Tvrtka";
                }
            }
        }      
    }
}
