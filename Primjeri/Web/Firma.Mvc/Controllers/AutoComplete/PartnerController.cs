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
    public class PartnerController : Controller
    {
        private readonly FirmaContext ctx;
        private readonly AppSettings appData;

        public PartnerController(FirmaContext ctx, IOptionsSnapshot<AppSettings> options)
        {
            this.ctx = ctx;
            appData = options.Value;
        }
        
        [HttpGet]      
        public IEnumerable<IdLabel> Get(string term)
        {                       
            var query = ctx.vw_Partner                                                        
                            .Select(p => new IdLabel
                            {
                                Id = p.IdPartnera,
                                Label = p.Naziv + " (" + p.OIB + ")"
                            })
                            .Where(l => l.Label.Contains(term));
          
            var list = query.OrderBy(l => l.Label)
                            .ThenBy(l => l.Id)
                            .Take(appData.AutoCompleteCount)
                            .ToList();

            //var queryOsobe = ctx.Osoba                                                      
            //                    .Select(o => new IdLabel
            //                    {
            //                        Id = o.IdOsobe,
            //                        Label = o.PrezimeOsobe + ", " + o.ImeOsobe + " (" + o.IdOsobeNavigation.Oib +")"
            //                    })
            //                    .Where(l => l.Label.Contains(term));

            //var queryPartneri = ctx.Tvrtka
            //                        .Select(t => new IdLabel
            //                        {
            //                            Id = t.IdTvrtke,
            //                            Label = t.NazivTvrtke + ", " + " (" + t.IdTvrtkeNavigation.Oib + ")"
            //                        })
            //                        .Where(l => l.Label.Contains(term));
            //var list = queryOsobe.Union(queryPartneri)
            //                     .OrderBy(l => l.Label)
            //                     .ThenBy(l => l.Id)
            //                     .ToList();
            return list;
        }       
    }
}
