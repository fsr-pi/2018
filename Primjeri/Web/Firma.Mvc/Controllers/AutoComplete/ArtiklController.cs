using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Firma.Mvc.Models;
using Microsoft.Extensions.Options;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Firma.Mvc.Controllers.AutoComplete
{
    [Route("autocomplete/[controller]")]
    public class ArtiklController : Controller
    {
        private readonly FirmaContext ctx;
        private readonly AppSettings appData;

        public ArtiklController(FirmaContext ctx, IOptionsSnapshot<AppSettings> options)
        {
            this.ctx = ctx;
            appData = options.Value;
        }
        
        [HttpGet]      
        public IEnumerable<Artikl> Get(string term)
        {
            var query = ctx.Artikl
                           .Where(a => a.NazArtikla.Contains(term))
                           .OrderBy(a => a.NazArtikla)                           
                           .Select(a => new Artikl
                           {
                               Id = a.SifArtikla,
                               Label = a.NazArtikla,
                               Cijena = a.CijArtikla
                           });
            var list = query.Take(appData.AutoCompleteCount).ToList();
            return list;
        }       
    }
}
