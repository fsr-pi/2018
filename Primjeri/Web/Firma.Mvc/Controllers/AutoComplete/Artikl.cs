namespace Firma.Mvc.Controllers.AutoComplete
{
    public class Artikl
    {
        public string Label { get; set; }
        public int Id { get; set; }
        public decimal Cijena { get; set; }
        public Artikl() { }
        public Artikl(int id, string label, decimal cijena)
        {
            Id = id;
            Label = label;
            Cijena = cijena;
        }        
    }
}
