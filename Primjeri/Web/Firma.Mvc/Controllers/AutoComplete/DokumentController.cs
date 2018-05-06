using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Firma.Mvc.Models;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Firma.Mvc.Controllers.AutoComplete
{
    [Route("autocomplete/[controller]")]
    public class DokumentController : Controller
    {
        private readonly FirmaContext ctx;
        private readonly AppSettings appData;

        public DokumentController(FirmaContext ctx, IOptionsSnapshot<AppSettings> options)
        {
            this.ctx = ctx;
            appData = options.Value;
        }
        
        [HttpGet]      
        public IEnumerable<IdLabel> Get(string term)
        {                       
            var query = ctx.ViewDokumentInfo                            
                            .FromSql(Constants.SqlViewDokumenti)                            
                            .Select(p => new IdLabel
                            {
                                Id = p.IdDokumenta,
                                Label = p.IdDokumenta + " " + p.NazPartnera + " " + p.IznosDokumenta                                                                             
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
