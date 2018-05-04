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
  public class DrzavaController : Controller
  {
    private readonly FirmaContext ctx;
    private readonly AppSettings appData;

    public DrzavaController(FirmaContext ctx, IOptions<AppSettings> options)
    {
      this.ctx = ctx;
      appData = options.Value;
    }

    //public IActionResult Index()
    //{
    //  var drzave = ctx.Drzava
    //                  .AsNoTracking()
    //                  .OrderBy(d => d.NazDrzave)
    //                  .ToList();
    //  return View("IndexSimple", drzave);
    //}


    public IActionResult Index(int page = 1, int sort = 1, bool ascending = true)
    {
      int pagesize = appData.PageSize;

      var query = ctx.Drzava
                  .AsNoTracking();

      int count = query.Count();
      if (count == 0)
      {
        TempData[Constants.Message] = "Ne postoji niti jedna država.";
        TempData[Constants.ErrorOccurred] = false;
        return RedirectToAction(nameof(Create));
      }

      var pagingInfo = new PagingInfo
      {
        CurrentPage = page,
        Sort = sort,
        Ascending = ascending,
        ItemsPerPage = pagesize,
        TotalItems = count
      };
      if (page > pagingInfo.TotalPages)
      {
        return RedirectToAction(nameof(Index), new { page = pagingInfo.TotalPages, sort, ascending });
      }

      System.Linq.Expressions.Expression<Func<Drzava, object>> orderSelector = null;
      switch (sort)
      {
        case 1:
          orderSelector = d => d.OznDrzave;
          break;
        case 2:
          orderSelector = d => d.NazDrzave;
          break;
        case 3:
          orderSelector = d => d.Iso3drzave;
          break;
        case 4:
          orderSelector = d => d.SifDrzave;
          break;
      }
      if (orderSelector != null)
      {
        query = ascending ?
               query.OrderBy(orderSelector) :
               query.OrderByDescending(orderSelector);
      }
      var drzave = query
                  .Skip((page - 1) * pagesize)
                  .Take(pagesize)
                  .ToList();
      var model = new DrzaveViewModel
      {
        Drzave = drzave,
        PagingInfo = pagingInfo
      };

      return View(model);
    }

    [HttpGet]
    public IActionResult Create()
    {
      return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(Drzava drzava)
    {
      if (ModelState.IsValid)
      {
        try
        {
          ctx.Add(drzava);
          ctx.SaveChanges();

          TempData[Constants.Message] = $"Država {drzava.NazDrzave} dodana.";
          TempData[Constants.ErrorOccurred] = false;
          return RedirectToAction(nameof(Index));
        }
        catch (Exception exc)
        {
          ModelState.AddModelError(string.Empty, exc.CompleteExceptionMessage());
          return View(drzava);
        }
      }
      else
      {
        return View(drzava);
      }
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Delete(string OznDrzave, int page = 1, int sort = 1, bool ascending = true)
    {
      var drzava = ctx.Drzava
                       .AsNoTracking()
                       .Where(d => d.OznDrzave == OznDrzave)
                       .SingleOrDefault();
      if (drzava != null)
      {
        try
        {
          string naziv = drzava.NazDrzave;
          ctx.Remove(drzava);
          ctx.SaveChanges();
          TempData[Constants.Message] = $"Država {naziv} uspješno obrisana";
          TempData[Constants.ErrorOccurred] = false;
        }
        catch (Exception exc)
        {
          TempData[Constants.Message] = "Pogreška prilikom brisanja države: " + exc.CompleteExceptionMessage();
          TempData[Constants.ErrorOccurred] = true;
        }
      }
      else
      {
        TempData[Constants.Message] = "Ne postoji država s oznakom: " + OznDrzave;
        TempData[Constants.ErrorOccurred] = true;
      }
      return RedirectToAction(nameof(Index), new { page = page, sort = sort, ascending = ascending });
    }

    [HttpGet]
    public IActionResult Edit(String id, int page = 1, int sort = 1, bool ascending = true)
    {
      var drzava = ctx.Drzava.AsNoTracking().Where(d => d.OznDrzave == id).SingleOrDefault();
      if (drzava == null)
      {
        return NotFound("Ne postoji država s oznakom: " + id);
      }
      else
      {
        ViewBag.Page = page;
        ViewBag.Sort = sort;
        ViewBag.Ascending = ascending;
        return View(drzava);
      }
    }

    [HttpPost, ActionName("Edit")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Update(string id, int page = 1, int sort = 1, bool ascending = true)
    {
      //za različite mogućnosti ažuriranja pogledati
      //attach, update, samo id, ...
      //https://docs.microsoft.com/en-us/aspnet/core/data/ef-mvc/crud#update-the-edit-page

      try
      {
        Drzava drzava = await ctx.Drzava.FindAsync(id);                          
        if (drzava == null)
        {
          return NotFound("Neispravna oznaka države: " + id);
        }

        if (await TryUpdateModelAsync<Drzava>(drzava, "",
            d => d.NazDrzave, d => d.SifDrzave, d => d.Iso3drzave
        ))
        {
          ViewBag.Page = page;
          ViewBag.Sort = sort;
          ViewBag.Ascending = ascending;
          try
          {
            await ctx.SaveChangesAsync();
            TempData[Constants.Message] = "Država ažurirana.";
            TempData[Constants.ErrorOccurred] = false;
            return RedirectToAction(nameof(Index), new { page = page, sort = sort, ascending = ascending });
          }
          catch (Exception exc)
          {
            ModelState.AddModelError(string.Empty, exc.CompleteExceptionMessage());
            return View(drzava);
          }
        }
        else
        {
          ModelState.AddModelError(string.Empty, "Podatke o državi nije moguće povezati s forme");
          return View(drzava);
        }
      }
      catch (Exception exc)
      {
        TempData[Constants.Message] = exc.CompleteExceptionMessage();
        TempData[Constants.ErrorOccurred] = true;
        return RedirectToAction(nameof(Edit), id);
      }
    }
  }
}
