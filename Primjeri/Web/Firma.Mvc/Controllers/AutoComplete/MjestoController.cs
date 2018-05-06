using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Firma.Mvc.Models;
using Microsoft.Extensions.Options;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Firma.Mvc.Controllers.AutoComplete
{
    [Route("autocomplete/[controller]")]
    public class MjestoController : Controller
    {
        private readonly FirmaContext ctx;
        private readonly AppSettings appData;

        public MjestoController(FirmaContext ctx, IOptionsSnapshot<AppSettings> options)
        {
            this.ctx = ctx;
            appData = options.Value;
        }
        
        [HttpGet]      
        public IEnumerable<IdLabel> Get(string term)
        {
            var query = ctx.Mjesto
                            .Select(m => new IdLabel
                            {
                                Id = m.IdMjesta,
                                Label = m.PostBrMjesta + " " + m.NazMjesta
                            })
                            .Where(l => l.Label.Contains(term));
          
            var list = query.OrderBy(l => l.Label)
                            .ThenBy(l => l.Id)
                            .Take(appData.AutoCompleteCount)
                            .ToList();           
            return list;
        }       
    }
}
