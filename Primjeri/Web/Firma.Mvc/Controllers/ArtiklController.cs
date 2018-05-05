using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using Firma.Mvc.Extensions;
using Firma.Mvc.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using Firma.Mvc.ViewModels;
using System.Threading.Tasks;

namespace Firma.Mvc.Controllers
{
  public class ArtiklController : Controller
  {
    private readonly FirmaContext ctx;
    private readonly AppSettings appData;

    public ArtiklController(FirmaContext ctx, IOptionsSnapshot<AppSettings> options)
    {
      this.ctx = ctx;
      appData = options.Value;
    }

    public IActionResult Index(int page = 1, int sort = 1, bool ascending = true)
    {
      int pagesize = appData.PageSize;
      var query = ctx.Artikl.AsNoTracking();
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
        return RedirectToAction(nameof(Index), new { page = pagingInfo.TotalPages, sort, ascending });
      }

      System.Linq.Expressions.Expression<Func<Artikl, object>> orderSelector = null;
      switch (sort)
      {
        //po slici se ne može sortirati, pa zato nema case 1:
        case 2:
          orderSelector = a => a.SifArtikla;
          break;
        case 3:
          orderSelector = a => a.NazArtikla;
          break;
        case 4:
          orderSelector = a => a.JedMjere;
          break;
        case 5:
          orderSelector = a => a.CijArtikla;
          break;
        case 6:
          orderSelector = a => a.ZastUsluga;
          break;
      }
      if (orderSelector != null)
      {
        query = ascending ?
               query.OrderBy(orderSelector) :
               query.OrderByDescending(orderSelector);
      }

      var artikli = query
                    .Select(a => new ArtiklViewModel
                    {
                      SifraArtikla = a.SifArtikla,
                      NazivArtikla = a.NazArtikla,
                      JedinicaMjere = a.JedMjere,
                      CijenaArtikla = a.CijArtikla,
                      Usluga = a.ZastUsluga,
                      TekstArtikla = a.TekstArtikla,
                      ImaSliku = a.SlikaArtikla != null,
                      ImageHash = a.SlikaArtikla != null ? a.SlikaArtikla.Length : 0
                    })
                    .Skip((page - 1) * pagesize)
                    .Take(pagesize)
                    .ToList();
      var model = new ArtikliViewModel
      {
        Artikli = artikli,
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
    public async Task<IActionResult> Create(Artikl artikl, IFormFile slika)
    {
      if (artikl.CijArtikla <= 0)
      {
        ModelState.AddModelError(nameof(Artikl.CijArtikla), "Cijena mora biti pozitivni broj");
      }
      else
      {
        //provjeri jedinstvenost šifre artikla
        bool exists = await ctx.Artikl.AnyAsync(a => a.SifArtikla == artikl.SifArtikla);
        if (exists)
        {
          ModelState.AddModelError(nameof(Artikl.SifArtikla), "Artikl s navedenom šifrom već postoji");
        }
      }
      if (ModelState.IsValid)
      {
        try
        {
          if (slika != null && slika.Length > 0)
          {
            using (MemoryStream stream = new MemoryStream())
            {
              await slika.CopyToAsync(stream);
              artikl.SlikaArtikla = stream.ToArray();
            }
          }
          ctx.Add(artikl);
          await ctx.SaveChangesAsync();

          TempData[Constants.Message] = $"Artikl  {artikl.SifArtikla} - {artikl.NazArtikla} dodan";
          TempData[Constants.ErrorOccurred] = false;
          return RedirectToAction(nameof(Index));

        }
        catch (Exception exc)
        {
          ModelState.AddModelError(string.Empty, exc.CompleteExceptionMessage());
          return View(artikl);
        }
      }
      else
      {
        return View(artikl);
      }
    }

    public FileContentResult GetImage(int id)
    {
      byte[] image = ctx.Artikl
                        .Where(a => a.SifArtikla == id)
                        .Select(a => a.SlikaArtikla)
                        .SingleOrDefault();
      if (image != null)
        return File(image, "image/jpeg");
      else
        return null;
    }

    [HttpGet]
    public IActionResult Edit(int id)
    {
      var artikl = ctx.Artikl.Find(id);
      if (artikl != null)
      {
        return PartialView(artikl);
      }
      else
      {
        return NotFound($"Neispravna šifra artikla: {id}");
      }
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(Artikl artikl, IFormFile slika, bool obrisiSliku)
    {
      if (artikl == null)
      {
        return NotFound("Nema poslanih podataka");
      }
      Artikl dbArtikl = ctx.Artikl.FirstOrDefault(a => a.SifArtikla == artikl.SifArtikla);
      if (dbArtikl == null)
      {
        return NotFound($"Neispravna šifra artikla: {artikl.SifArtikla}");
      }

      if (artikl.CijArtikla <= 0)
      {
        ModelState.AddModelError(nameof(Artikl.CijArtikla), "Cijena mora biti pozitivni broj");
      }

      if (ModelState.IsValid)
      {
        try
        {
          //ne možemo ići na varijantu ctx.Update(artikl), jer nismo prenosili sliku, pa bi bila obrisana
          dbArtikl.JedMjere = artikl.JedMjere;
          dbArtikl.NazArtikla = artikl.NazArtikla;
          dbArtikl.ZastUsluga = artikl.ZastUsluga;
          dbArtikl.TekstArtikla = artikl.TekstArtikla;
          dbArtikl.CijArtikla = artikl.CijArtikla;

          if (slika != null && slika.Length > 0)
          {
            using (MemoryStream stream = new MemoryStream())
            {
              slika.CopyTo(stream);
              dbArtikl.SlikaArtikla = stream.ToArray();
            }
          }
          else if (obrisiSliku)
          {
            dbArtikl.SlikaArtikla = null;
          }

          ctx.SaveChanges();
          return StatusCode(302, Url.Action(nameof(Row), new { id = dbArtikl.SifArtikla }));
        }
        catch (Exception exc)
        {
          ModelState.AddModelError(string.Empty, exc.CompleteExceptionMessage());
          return PartialView(artikl);
        }
      }
      else
      {
        return PartialView(artikl);
      }
    }

    public PartialViewResult Row(int id)
    {
      var artikl = ctx.Artikl
                       .AsNoTracking()
                       .Where(a => a.SifArtikla == id)
                       .Select(a => new ArtiklViewModel
                       {
                         SifraArtikla = a.SifArtikla,
                         NazivArtikla = a.NazArtikla,
                         JedinicaMjere = a.JedMjere,
                         CijenaArtikla = a.CijArtikla,
                         Usluga = a.ZastUsluga,
                         TekstArtikla = a.TekstArtikla,
                         ImaSliku = a.SlikaArtikla != null,
                         ImageHash = a.SlikaArtikla != null ? a.SlikaArtikla.Length : 0
                       })
                       .SingleOrDefault();
      if (artikl != null)
      {
        return PartialView(artikl);
      }
      else
      {
        //vratiti prazan sadržaj?
        return PartialView("ErrorMessageRow", $"Neispravna šifra artikla: {id}");
      }
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Delete(int id)
    {
      var artikl = ctx.Artikl.Find(id);
      if (artikl != null)
      {
        try
        {
          string naziv = artikl.NazArtikla;
          ctx.Remove(artikl);
          ctx.SaveChanges();
          var result = new
          {
            message = $"Artikl {naziv} sa šifrom {id} uspješno obrisan.",
            successful = true
          };
          return Json(result);
        }
        catch (Exception exc)
        {
          var result = new
          {
            message = "Pogreška prilikom brisanja artikla: " + exc.CompleteExceptionMessage(),
            successful = false
          };
          return Json(result);
        }
      }
      else
      {
        return NotFound($"Artikl sa šifrom {id} ne postoji");
      }
    }
  }
}
