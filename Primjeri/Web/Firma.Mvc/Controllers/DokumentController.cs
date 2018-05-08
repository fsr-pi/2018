using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Firma.Mvc.Models;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using Firma.Mvc.ViewModels;
using Firma.Mvc.Extensions;

namespace Firma.Mvc.Controllers
{
  public class DokumentController : Controller
  {
    private readonly FirmaContext ctx;
    private readonly AppSettings appData;

    public DokumentController(FirmaContext ctx, IOptionsSnapshot<AppSettings> options)
    {
      this.ctx = ctx;
      appData = options.Value;
    }

    public IActionResult Index(string filter, int page = 1, int sort = 1, bool ascending = true)
    {

      int pagesize = appData.PageSize;

      var query = ctx.ViewDokumentInfo.AsNoTracking()
                              .FromSql(Constants.SqlViewDokumenti);

      DokumentFilter df = new DokumentFilter();
      if (!string.IsNullOrWhiteSpace(filter))
      {
        df = DokumentFilter.FromString(filter);
        if (!df.IsEmpty())
        {
          if (df.IdPartnera.HasValue)
          {
            df.NazPartnera = ctx.vw_Partner
                                .Where(p => p.IdPartnera == df.IdPartnera)
                                .Select(vp => vp.Naziv)
                                .FirstOrDefault();
          }
          query = df.Apply(query);
        }
      }

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
        //jesmo li van raspona zbog filtera? Ako da prikaži prazne podatke, i sačuvaj filter
        if (page != 1 || df.IsEmpty())
        {
          return RedirectToAction(nameof(Index), new { page = pagingInfo.TotalPages, sort, ascending, filter });
        }        
      }

      query = ApplySort(sort, ascending, query);

      var dokumenti = query
                  .Skip((page - 1) * pagesize)
                  .Take(pagesize)
                  .ToList();
      for (int i = 0; i < dokumenti.Count; i++)
      {
        dokumenti[i].Position = (page - 1) * pagesize + i;
      }
      var model = new DokumentiViewModel
      {
        Dokumenti = dokumenti,
        PagingInfo = pagingInfo,
        Filter = df
      };

      return View(model);
    }



    [HttpPost]
    public IActionResult Filter(DokumentFilter filter)
    {
      return RedirectToAction(nameof(Index), new { filter = filter.ToString() });
    }

    public IActionResult Show(int id, int position, string filter, int page = 1, int sort = 1, bool ascending = true, string viewName = nameof(Show))
    {
      ViewBag.Page = page;
      ViewBag.Sort = sort;
      ViewBag.Ascending = ascending;
      ViewBag.Filter = filter;
      ViewBag.Position = position;

      var dokument = ctx.Dokument
                        .Where(d => d.IdDokumenta == id)
                        .Select(d => new DokumentViewModel
                        {
                          BrDokumenta = d.BrDokumenta,
                          DatDokumenta = d.DatDokumenta,
                          IdDokumenta = d.IdDokumenta,
                          IdPartnera = d.IdPartnera,
                          IdPrethDokumenta = d.IdPrethDokumenta,
                          IznosDokumenta = d.IznosDokumenta,
                          PostoPorez = d.PostoPorez,
                          VrDokumenta = d.VrDokumenta
                        })
                        .FirstOrDefault();
      if (dokument == null)
      {
        return NotFound($"Dokument {id} ne postoji");
      }
      else
      {
        SetPreviousAndNext(position, filter, sort, ascending);
        string tipPartnera = ctx.Partner
                                .Where(p => p.IdPartnera == dokument.IdPartnera)
                                .Select(p => p.TipPartnera)
                                .Single();
        if (tipPartnera == "O")
        {
          dokument.NazPartnera = ctx.Osoba
                                .Where(p => p.IdOsobe == dokument.IdPartnera)
                                .Select(p => p.PrezimeOsobe + ", " + p.ImeOsobe + " (" + p.IdOsobeNavigation.Oib + ")")
                                .Single();
        }
        else
        {
          dokument.NazPartnera = ctx.Tvrtka
                                .Where(p => p.IdTvrtke == dokument.IdPartnera)
                                .Select(p => p.NazivTvrtke + " (" + p.IdTvrtkeNavigation.Oib + ")")
                                .Single();
        }
        if (dokument.IdPrethDokumenta.HasValue)
        {
          dokument.NazPrethodnogDokumenta = ctx.ViewDokumentInfo
                                           .FromSql(Constants.SqlViewDokumenti)
                                           .Where(d => d.IdDokumenta == dokument.IdPrethDokumenta)
                                           .Select(d => d.IdDokumenta + " " + d.NazPartnera + " " + d.IznosDokumenta)
                                           .FirstOrDefault();
        }
        //učitavanje stavki
        var stavke = ctx.Stavka
                        .Where(s => s.IdDokumenta == dokument.IdDokumenta)
                        .OrderBy(s => s.IdStavke)
                        .Select(s => new StavkaViewModel
                        {
                          IdStavke = s.IdStavke,
                          JedCijArtikla = s.JedCijArtikla,
                          KolArtikla = s.KolArtikla,
                          NazArtikla = s.SifArtiklaNavigation.NazArtikla,
                          PostoRabat = s.PostoRabat,
                          SifArtikla = s.SifArtikla
                        })
                        .ToList();
        dokument.Stavke = stavke;
        return View(viewName, dokument);
      }
    }

    [HttpGet]
    public IActionResult Create()
    {
      int maxbr = ctx.Dokument.Max(d => d.BrDokumenta) + 1; //samo za primjer, inače u stvarnosti može biti paralelnih korisnika
      var dokument = new DokumentViewModel
      {
        DatDokumenta = DateTime.Now,
        BrDokumenta = maxbr
      };
      return View(dokument);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(DokumentViewModel model)
    {
      if (ModelState.IsValid)
      {
        Dokument d = new Dokument();
        d.BrDokumenta = model.BrDokumenta;
        d.DatDokumenta = model.DatDokumenta.Date;
        d.IdPartnera = model.IdPartnera.Value;
        d.IdPrethDokumenta = model.IdPrethDokumenta;
        d.PostoPorez = model.PostoPorez;
        d.VrDokumenta = model.VrDokumenta;
        foreach (var stavka in model.Stavke)
        {
          Stavka novaStavka = new Stavka();
          novaStavka.SifArtikla = stavka.SifArtikla;
          novaStavka.KolArtikla = stavka.KolArtikla;
          novaStavka.PostoRabat = stavka.PostoRabat;
          novaStavka.JedCijArtikla = stavka.JedCijArtikla;
          d.Stavka.Add(novaStavka);          
        }

        d.IznosDokumenta = (1 + d.PostoPorez)
                            * d.Stavka.Sum(s => s.KolArtikla * (1 - s.PostoRabat) * s.JedCijArtikla);
        //eventualno umanji iznos za dodatni popust za kupca i sl... nešto što bi bilo poslovno pravilo
        try
        {
          ctx.Add(d);
          ctx.SaveChanges();

          TempData[Constants.Message] = $"Dokument uspješno dodan. Id={d.IdDokumenta}";
          TempData[Constants.ErrorOccurred] = false;
          return RedirectToAction(nameof(Edit), new { id = d.IdDokumenta });

        }
        catch (Exception exc)
        {
          ModelState.AddModelError(string.Empty, exc.CompleteExceptionMessage());
          return View(model);
        }
      }
      else
      {
        return View(model);
      }
    }

    [HttpGet]
    public IActionResult Edit(int id, int position, string filter, int page = 1, int sort = 1, bool ascending = true)
    {
      return Show(id, position, filter, page, sort, ascending, viewName: nameof(Edit));
    }



    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(DokumentViewModel model, int position, string filter, int page = 1, int sort = 1, bool ascending = true)
    {
      ViewBag.Page = page;
      ViewBag.Sort = sort;
      ViewBag.Ascending = ascending;
      ViewBag.Filter = filter;
      ViewBag.Position = position;
      if (ModelState.IsValid)
      {
        var dokument = ctx.Dokument
                          .Include(d => d.Stavka)
                          .Where(d => d.IdDokumenta == model.IdDokumenta)
                          .FirstOrDefault();
        if (dokument == null)
        {
          return NotFound("Ne postoji dokument s id-om: " + model.IdDokumenta);
        }

        SetPreviousAndNext(position, filter, sort, ascending);
        dokument.BrDokumenta = model.BrDokumenta;
        dokument.DatDokumenta = model.DatDokumenta.Date;
        dokument.IdPartnera = model.IdPartnera.Value;
        dokument.IdPrethDokumenta = model.IdPrethDokumenta;
        dokument.PostoPorez = model.StopaPoreza / 100m;
        dokument.VrDokumenta = model.VrDokumenta;

        List<int> idStavki = model.Stavke
                                  .Where(s => s.IdStavke > 0)
                                  .Select(s => s.IdStavke)
                                  .ToList();
        //izbaci sve koje su nisu više u modelu
        ctx.RemoveRange(dokument.Stavka.Where(s => !idStavki.Contains(s.IdStavke)));

        foreach (var stavka in model.Stavke)
        {
          //ažuriraj postojeće i dodaj nove
          Stavka novaStavka;
          if (stavka.IdStavke > 0)
          {
            novaStavka = dokument.Stavka.First(s => s.IdStavke == stavka.IdStavke);
          }
          else
          {
            novaStavka = new Stavka();
            dokument.Stavka.Add(novaStavka);
          }
          novaStavka.SifArtikla = stavka.SifArtikla;
          novaStavka.KolArtikla = stavka.KolArtikla;
          novaStavka.PostoRabat = stavka.PostoRabat;
          novaStavka.JedCijArtikla = stavka.JedCijArtikla;
        }

        dokument.IznosDokumenta = (1 + dokument.PostoPorez) *
                                  model.Stavke.Sum(s => s.KolArtikla * (1 - s.PostoRabat) * s.JedCijArtikla);
        //eventualno umanji iznos za dodatni popust za kupca i sl... nešto što bi bilo poslovno pravilo
        try
        {

          ctx.SaveChanges();

          TempData[Constants.Message] = $"Dokument {dokument.IdDokumenta} uspješno ažuriran.";
          TempData[Constants.ErrorOccurred] = false;
          return RedirectToAction(nameof(Edit), new
          {
            id = dokument.IdDokumenta,
            position,
            filter,
            page,
            sort,
            ascending
          });

        }
        catch (Exception exc)
        {
          ModelState.AddModelError(string.Empty, exc.CompleteExceptionMessage());
          return View(model);
        }
      }
      else
      {
        return View(model);
      }
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int IdDokumenta, string filter, int page = 1, int sort = 1, bool ascending = true)
    {
      var dokument = await ctx.Dokument
                       .AsNoTracking()
                       .Where(d => d.IdDokumenta == IdDokumenta)
                       .SingleOrDefaultAsync();
      if (dokument != null)
      {
        try
        {
          ctx.Remove(dokument);
          await ctx.SaveChangesAsync();
          TempData[Constants.Message] = $"Dokument {dokument.IdDokumenta} uspješno obrisan.";
          TempData[Constants.ErrorOccurred] = false;
        }
        catch (Exception exc)
        {
          TempData[Constants.Message] = "Pogreška prilikom brisanja dokumenta: " + exc.CompleteExceptionMessage();
          TempData[Constants.ErrorOccurred] = true;
        }
      }
      else
      {
        TempData[Constants.Message] = "Ne postoji dokument s id-om: " + IdDokumenta;
        TempData[Constants.ErrorOccurred] = true;
      }
      return RedirectToAction(nameof(Index), new { filter = filter, page = page, sort = sort, ascending = ascending });
    }

    private static IQueryable<ViewDokumentInfo> ApplySort(int sort, bool ascending, IQueryable<ViewDokumentInfo> query)
    {
      System.Linq.Expressions.Expression<Func<ViewDokumentInfo, object>> orderSelector = null;
      switch (sort)
      {
        case 1:
          orderSelector = d => d.IdDokumenta;
          break;
        case 2:
          orderSelector = d => d.NazPartnera;
          break;
        case 3:
          orderSelector = d => d.DatDokumenta;
          break;
        case 4:
          orderSelector = d => d.IznosDokumenta;
          break;
      }
      if (orderSelector != null)
      {
        query = ascending ?
               query.OrderBy(orderSelector) :
               query.OrderByDescending(orderSelector);
      }

      return query;
    }

    private void SetPreviousAndNext(int position, string filter, int sort, bool ascending)
    {
      var query = ctx.ViewDokumentInfo
                     .FromSql(Constants.SqlViewDokumenti)
                     .AsNoTracking();


      DokumentFilter df = new DokumentFilter();
      if (!string.IsNullOrWhiteSpace(filter))
      {
        df = DokumentFilter.FromString(filter);
        if (!df.IsEmpty())
        {
          query = df.Apply(query);
        }
      }

      query = ApplySort(sort, ascending, query);      
      if (position > 0)
      {
        ViewBag.Previous = query.Skip(position - 1).Select(d => d.IdDokumenta).First();
      }
      if (position < query.Count() - 1) //TO DO prenesi Count kao parametar
      {
        ViewBag.Next = query.Skip(position + 1).Select(d => d.IdDokumenta).First();
      }
    }
  }
}
