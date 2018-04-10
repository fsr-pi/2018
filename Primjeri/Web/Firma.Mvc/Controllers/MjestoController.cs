using Firma.Mvc.Extensions;
using Firma.Mvc.Models;
using Firma.Mvc.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Firma.Mvc.Controllers
{
  public class MjestoController : Controller
  {
    private readonly FirmaContext ctx;
    private readonly AppSettings appData;

    public MjestoController(FirmaContext ctx, IOptions<AppSettings> options)
    {
      this.ctx = ctx;
      appData = options.Value;
    }

    public IActionResult Index()
    {
      var mjesta = ctx.Mjesto
                      .OrderBy(m => m.OznDrzaveNavigation.NazDrzave)
                      .ThenBy(m => m.PostBrMjesta)
                      .Select(m => new MjestoViewModel
                      {
                        IdMjesta = m.IdMjesta,
                        NazivMjesta = m.NazMjesta,
                        PostBrojMjesta = m.PostBrMjesta,
                        PostNazivMjesta = m.PostNazMjesta,
                        NazivDrzave = m.OznDrzaveNavigation.NazDrzave
                      })                      
                      .ToList();
      return View(mjesta);
    }
  }
}
