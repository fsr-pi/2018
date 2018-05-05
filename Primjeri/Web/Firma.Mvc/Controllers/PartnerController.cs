using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Firma.Mvc.Models;
using Microsoft.Extensions.Options;
using Firma.Mvc.ViewModels;
using Firma.Mvc.Extensions;
using Microsoft.EntityFrameworkCore;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Firma.Mvc.Controllers
{
  public class PartnerController : Controller
  {
    private readonly FirmaContext ctx;
    private readonly AppSettings appData;

    public PartnerController(FirmaContext ctx, IOptionsSnapshot<AppSettings> options)
    {
      this.ctx = ctx;
      appData = options.Value;
    }

    public IActionResult Index(string filter, int page = 1, int sort = 1, bool ascending = true)
    {
      int pagesize = appData.PageSize;
      //var sql = "SELECT * FROM vw_partner"; //potrebbon ako se ne zove vw_Partner u FirmaContext
      var query = ctx.vw_Partner
                     .AsNoTracking();
                      //.FromSql(sql);


      int count = query.Count();

      var pagingInfo = new PagingInfo
      {
        CurrentPage = page,
        Sort = sort,
        Ascending = ascending,
        ItemsPerPage = pagesize,
        TotalItems = count
      };
      if (page < 1)
      {
        page = 1;
      }
      else if (page > pagingInfo.TotalPages)
      {
        return RedirectToAction(nameof(Index), new { page = pagingInfo.TotalPages, sort = sort, ascending = ascending });
      }

      System.Linq.Expressions.Expression<Func<ViewPartner, object>> orderSelector = null;
      switch (sort)
      {
        case 1:
          orderSelector = p => p.IdPartnera;
          break;
        case 2:
          orderSelector = p => p.TipPartnera;
          break;
        case 3:
          orderSelector = p => p.OIB;
          break;
        case 4:
          orderSelector = p => p.Naziv;
          break;
      }
      if (orderSelector != null)
      {
        query = ascending ?
               query.OrderBy(orderSelector) :
               query.OrderByDescending(orderSelector);
      }

      var partneri = query
                  .Skip((page - 1) * pagesize)
                  .Take(pagesize)
                  .ToList();
      var model = new PartneriViewModel
      {
        Partneri = partneri,
        PagingInfo = pagingInfo
      };

      return View(model);
    }



    [HttpGet]
    public IActionResult Create()
    {
      PartnerViewModel model = new PartnerViewModel
      {
        TipPartnera = "O"
      };
      return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(PartnerViewModel model)
    {
      ValidateModel(model);
      if (ModelState.IsValid)
      {
        Partner p = new Partner();
        p.TipPartnera = model.TipPartnera;
        CopyValues(p, model);
        try
        {
          ctx.Add(p);
          ctx.SaveChanges();

          TempData[Constants.Message] = $"Partner uspješno dodan. Id={p.IdPartnera}";
          TempData[Constants.ErrorOccurred] = false;
          return RedirectToAction(nameof(Index));

        }
        catch (Exception exc)
        {
          DohvatiNaziveMjesta(model);
          ModelState.AddModelError(string.Empty, exc.CompleteExceptionMessage());
          return View(model);
        }
      }
      else
      {
        DohvatiNaziveMjesta(model);
        return View(model);
      }
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int IdPartnera, int page = 1, int sort = 1, bool ascending = true)
    {
      var partner = await ctx.Partner
                       .AsNoTracking()
                       .Where(p => p.IdPartnera == IdPartnera)
                       .SingleOrDefaultAsync();
      if (partner != null)
      {
        try
        {
          ctx.Remove(partner);
          await ctx.SaveChangesAsync();
          TempData[Constants.Message] = $"Partner {partner.IdPartnera} uspješno obrisan.";
          TempData[Constants.ErrorOccurred] = false;
        }
        catch (Exception exc)
        {
          TempData[Constants.Message] = "Pogreška prilikom brisanja partnera: " + exc.CompleteExceptionMessage();
          TempData[Constants.ErrorOccurred] = true;
        }
      }
      else
      {
        TempData[Constants.Message] = "Ne postoji partner s id-om: " + IdPartnera;
        TempData[Constants.ErrorOccurred] = true;
      }
      return RedirectToAction(nameof(Index), new { page = page, sort = sort, ascending = ascending });
    }

    [HttpGet]
    public IActionResult Edit(int id, int page = 1, int sort = 1, bool ascending = true)
    {
      var partner = ctx.Partner.Find(id);
      if (partner == null)
      {
        return NotFound("Ne postoji partner s oznakom: " + id);
      }
      else
      {
        PartnerViewModel model = new PartnerViewModel
        {
          IdPartnera = partner.IdPartnera,
          IdMjestaIsporuke = partner.IdMjestaIsporuke,
          IdMjestaPartnera = partner.IdMjestaPartnera,
          AdrIsporuke = partner.AdrIsporuke,
          AdrPartnera = partner.AdrPartnera,
          Oib = partner.Oib,
          TipPartnera = partner.TipPartnera
        };
        if (model.TipPartnera == "O")
        {
          Osoba osoba = ctx.Osoba.AsNoTracking()
                           .Where(o => o.IdOsobe == model.IdPartnera)
                           .First(); //Single()
          model.ImeOsobe = osoba.ImeOsobe;
          model.PrezimeOsobe = osoba.PrezimeOsobe;
        }
        else
        {
          Tvrtka tvrtka = ctx.Tvrtka.AsNoTracking()
                           .Where(t => t.IdTvrtke == model.IdPartnera)
                           .First(); //Single()
          model.MatBrTvrtke = tvrtka.MatBrTvrtke;
          model.NazivTvrtke = tvrtka.NazivTvrtke;
        }

        DohvatiNaziveMjesta(model);

        ViewBag.Page = page;
        ViewBag.Sort = sort;
        ViewBag.Ascending = ascending;
        return View(model);
      }
    }



    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(PartnerViewModel model, int page = 1, int sort = 1, bool ascending = true)
    {
      if (model == null)
      {
        return NotFound("Nema poslanih podataka");
      }
      var partner = ctx.Partner.Find(model.IdPartnera);
      if (partner == null)
      {
        return NotFound("Ne postoji partner s id-om: " + model.IdPartnera);
      }
      ValidateModel(model);

      if (ModelState.IsValid)
      {
        try
        {
          CopyValues(partner, model);

          //vezani dio je stvoren s new Osoba() ili new Tvrtka() pa je entity stated Added što bi proizvelo Insert pa ne update
          if (partner.Osoba != null)
          {
            ctx.Entry(partner.Osoba).State = EntityState.Modified;
          }
          if (partner.Tvrtka != null)
          {
            ctx.Entry(partner.Tvrtka).State = EntityState.Modified;
          }

          ctx.SaveChanges();
          TempData[Constants.Message] = $"Partner {model.IdPartnera} uspješno ažuriran";
          TempData[Constants.ErrorOccurred] = false;
          return RedirectToAction(nameof(Index), new { page = page, sort = sort, ascending = ascending });

        }
        catch (Exception exc)
        {
          ModelState.AddModelError(string.Empty, exc.CompleteExceptionMessage());
          DohvatiNaziveMjesta(model);
          return View(model);
        }
      }
      else
      {
        DohvatiNaziveMjesta(model);
        return View(model);
      }
    }


    #region Private methods
    private void CopyValues(Partner partner, PartnerViewModel model)
    {
      partner.AdrIsporuke = model.AdrIsporuke;
      partner.AdrPartnera = model.AdrPartnera;
      partner.IdMjestaIsporuke = model.IdMjestaIsporuke;
      partner.IdMjestaPartnera = model.IdMjestaPartnera;
      partner.Oib = model.Oib;
      if (partner.TipPartnera == "O")
      {
        partner.Osoba = new Osoba();
        partner.Osoba.ImeOsobe = model.ImeOsobe;
        partner.Osoba.PrezimeOsobe = model.PrezimeOsobe;
      }
      else
      {
        partner.Tvrtka = new Tvrtka();
        partner.Tvrtka.MatBrTvrtke = model.MatBrTvrtke;
        partner.Tvrtka.NazivTvrtke = model.NazivTvrtke;
      }

    }

    private void DohvatiNaziveMjesta(PartnerViewModel model)
    {
      try
      {
        //dohvati nazive mjesta                
        if (model.IdMjestaPartnera.HasValue)
        {
          var mjesto = ctx.Mjesto
                          .Where(m => m.IdMjesta == model.IdMjestaPartnera.Value)
                          .Select(m => new { m.PostBrMjesta, m.NazMjesta })
                          .First();
          model.NazMjestaPartnera = string.Format("{0} {1}", mjesto.PostBrMjesta, mjesto.NazMjesta);
        }
        if (model.IdMjestaIsporuke.HasValue)
        {
          var mjesto = ctx.Mjesto
                          .Where(m => m.IdMjesta == model.IdMjestaIsporuke.Value)
                          .Select(m => new { m.PostBrMjesta, m.NazMjesta })
                          .First();
          model.NazMjestaIsporuke = string.Format("{0} {1}", mjesto.PostBrMjesta, mjesto.NazMjesta);
        }
      }
      catch (Exception)
      {
        //TO DO Log error (npr. s NLogom)
        throw;
      }
    }

    private void ValidateModel(PartnerViewModel model)
    {
      if (model.TipPartnera == "O")
      {
        if (string.IsNullOrWhiteSpace(model.ImeOsobe))
        {
          ModelState.AddModelError(nameof(model.ImeOsobe), "Ime osoba ne smije biti prazno");
        }
        if (string.IsNullOrWhiteSpace(model.PrezimeOsobe))
        {
          ModelState.AddModelError(nameof(model.PrezimeOsobe), "Prezime osoba ne smije biti prazno");
        }
      }
      else
      {
        if (string.IsNullOrWhiteSpace(model.MatBrTvrtke))
        {
          ModelState.AddModelError(nameof(model.MatBrTvrtke), "Matični broj tvrtke mora biti popunjen");
        }
        if (string.IsNullOrWhiteSpace(model.NazivTvrtke))
        {
          ModelState.AddModelError(nameof(model.PrezimeOsobe), "Naziv tvrtke mora biti popunjen");
        }
      }
    }
    #endregion
  }
}