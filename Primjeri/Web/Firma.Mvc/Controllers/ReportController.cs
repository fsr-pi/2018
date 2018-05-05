using Firma.Mvc.Models;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PdfRpt.ColumnsItemsTemplates;
using PdfRpt.Core.Contracts;
using PdfRpt.Core.Helper;
using PdfRpt.FluentInterface;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Firma.Mvc.Controllers
{
  public class ReportController : Controller
  {
    private readonly FirmaContext ctx;

    public ReportController(FirmaContext ctx)
    {
      this.ctx = ctx;
    }

    public IActionResult Index()
    {
      return View();
    }

    public async Task<IActionResult> Drzave()
    {
      string naslov = "Popis država";
      var drzave = await ctx.Drzava
                            .AsNoTracking()
                            .OrderBy(d => d.NazDrzave)
                            .ToListAsync();
      PdfReport report = CreateReport(naslov);
      #region Podnožje i zaglavlje
      report.PagesFooter(footer =>
      {
        footer.DefaultFooter(DateTime.Now.ToString("dd.MM.yyyy."));
      })
      .PagesHeader(header =>
      {
        header.CacheHeader(cache: true); // It's a default setting to improve the performance.
        header.DefaultHeader(defaultHeader =>
        {
          defaultHeader.RunDirection(PdfRunDirection.LeftToRight);
          defaultHeader.Message(naslov);
        });
      });
      #endregion
      #region Postavljanje izvora podataka i stupaca
      report.MainTableDataSource(dataSource => dataSource.StronglyTypedList(drzave));

      report.MainTableColumns(columns =>
      {
        columns.AddColumn(column =>
        {
          column.IsRowNumber(true);
          column.CellsHorizontalAlignment(HorizontalAlignment.Right);
          column.IsVisible(true);
          column.Order(0);
          column.Width(1);
          column.HeaderCell("#", horizontalAlignment: HorizontalAlignment.Right);
        });

        columns.AddColumn(column =>
        {
          column.PropertyName(nameof(Drzava.OznDrzave));
          column.CellsHorizontalAlignment(HorizontalAlignment.Center);
          column.IsVisible(true);
          column.Order(1);
          column.Width(2);
          column.HeaderCell("Oznaka države");
        });

        columns.AddColumn(column =>
        {
          column.PropertyName<Drzava>(x => x.NazDrzave);
          column.CellsHorizontalAlignment(HorizontalAlignment.Center);
          column.IsVisible(true);
          column.Order(2);
          column.Width(3);
          column.HeaderCell("Naziv države", horizontalAlignment: HorizontalAlignment.Center);
        });

        columns.AddColumn(column =>
        {
          column.PropertyName<Drzava>(x => x.Iso3drzave);
          column.CellsHorizontalAlignment(HorizontalAlignment.Center);
          column.IsVisible(true);
          column.Order(3);
          column.Width(1);
          column.HeaderCell("ISO3", horizontalAlignment: HorizontalAlignment.Center);
        });

        columns.AddColumn(column =>
        {
          column.PropertyName<Drzava>(x => x.SifDrzave);
          column.CellsHorizontalAlignment(HorizontalAlignment.Center);
          column.IsVisible(true);
          column.Order(4);
          column.Width(1);
          column.HeaderCell("Šifra države", horizontalAlignment: HorizontalAlignment.Center);
        });
      });

      #endregion      
      byte[] pdf = report.GenerateAsByteArray();

      if (pdf != null)
      {
        Response.Headers.Add("content-disposition", "inline; filename=drzave.pdf");
        return File(pdf, "application/pdf");
        //return File(pdf, "application/pdf", "drzave.pdf"); //Otvara save as dialog
      }
      else
        return NotFound();
    }

    public async Task<IActionResult> Artikli()
    {
      string naslov = "Deset najskupljih artikala koji imaju sliku";
      var artikli = await ctx.Artikl
                            .AsNoTracking()
                            .Where(a => a.SlikaArtikla != null)
                            .OrderByDescending(a => a.CijArtikla)
                            .Select(a => new
                            {
                              a.SifArtikla,
                              a.NazArtikla,
                              a.CijArtikla,
                              a.SlikaArtikla
                            })
                            .Take(10)
                            .ToListAsync();
      PdfReport report = CreateReport(naslov);
      #region Podnožje i zaglavlje
      report.PagesFooter(footer =>
      {
        footer.DefaultFooter(DateTime.Now.ToString("dd.MM.yyyy."));
      })
      .PagesHeader(header =>
      {
        header.CacheHeader(cache: true); // It's a default setting to improve the performance.
        header.DefaultHeader(defaultHeader =>
        {
          defaultHeader.RunDirection(PdfRunDirection.LeftToRight);
          defaultHeader.Message(naslov);
        });
      });
      #endregion
      #region Postavljanje izvora podataka i stupaca
      report.MainTableDataSource(dataSource => dataSource.StronglyTypedList(artikli));

      report.MainTableSummarySettings(summarySettings =>
      {
        summarySettings.OverallSummarySettings("Ukupno");
      });

      report.MainTableColumns(columns =>
      {
        columns.AddColumn(column =>
        {
          column.IsRowNumber(true);
          column.CellsHorizontalAlignment(HorizontalAlignment.Right);
          column.IsVisible(true);
          column.Order(0);
          column.Width(1);
          column.HeaderCell("#", horizontalAlignment: HorizontalAlignment.Right);
        });

        columns.AddColumn(column =>
        {
          column.PropertyName(nameof(Artikl.SlikaArtikla));
          column.CellsHorizontalAlignment(HorizontalAlignment.Center);
          column.IsVisible(true);
          column.Order(1);
          column.Width(1);
          column.HeaderCell(" ");
          column.ColumnItemsTemplate(t => t.ByteArrayImage(string.Empty, fitImages: true));
        });

        columns.AddColumn(column =>
        {
          column.PropertyName(nameof(Artikl.SifArtikla));
          column.CellsHorizontalAlignment(HorizontalAlignment.Center);
          column.IsVisible(true);
          column.Order(2);
          column.Width(1);
          column.HeaderCell("Šifra");
        });

        columns.AddColumn(column =>
        {
          column.PropertyName<Artikl>(x => x.NazArtikla);
          column.CellsHorizontalAlignment(HorizontalAlignment.Center);
          column.IsVisible(true);
          column.Order(3);
          column.Width(4);
          column.HeaderCell("Naziv artikla", horizontalAlignment: HorizontalAlignment.Center);
        });

        columns.AddColumn(column =>
        {
          column.PropertyName<Artikl>(x => x.CijArtikla);
          column.CellsHorizontalAlignment(HorizontalAlignment.Center);
          column.IsVisible(true);
          column.Order(4);
          column.Width(1);
          column.HeaderCell("Cijena", horizontalAlignment: HorizontalAlignment.Center);
          column.ColumnItemsTemplate(template =>
          {
            template.TextBlock();
            template.DisplayFormatFormula(obj => obj == null || string.IsNullOrEmpty(obj.ToString())
                                                ? string.Empty : string.Format("{0:C2}", obj));
          });
          column.AggregateFunction(aggregateFunction =>
          {
            aggregateFunction.NumericAggregateFunction(AggregateFunction.Sum);
            aggregateFunction.DisplayFormatFormula(obj => obj == null || string.IsNullOrEmpty(obj.ToString())
                                                ? string.Empty : string.Format("{0:C2}", obj));
          });
        });
      });

      #endregion      
      byte[] pdf = report.GenerateAsByteArray();

      if (pdf != null)
      {
        Response.Headers.Add("content-disposition", "inline; filename=artikli.pdf");
        return File(pdf, "application/pdf");
      }
      else
        return NotFound();
    }


    public async Task<IActionResult> Dokumenti()
    {
      int n = 10;
      var param = new SqlParameter("N", n);
      string naslov = $"{n} najvećih kupnji";
      var stavke = await ctx.StavkaDenorm
                            .AsNoTracking()
                            .FromSql("SELECT * FROM fn_NajveceKupnje(@N)", param)
                            .OrderBy(s => s.IdDokumenta)
                            .ThenBy(s => s.NazArtikla)
                            .ToListAsync();
      stavke.ForEach(s => s.UrlDokumenta = Url.Action("Edit", "Dokument", new { id = s.IdDokumenta }));
      PdfReport report = CreateReport(naslov);
      #region Podnožje i zaglavlje
      report.PagesFooter(footer =>
      {
        footer.DefaultFooter(DateTime.Now.ToString("dd.MM.yyyy."));
      })
      .PagesHeader(header =>
      {
        header.CacheHeader(cache: true); // It's a default setting to improve the performance.
        header.CustomHeader(new MasterDetailsHeaders(naslov)
        {
          PdfRptFont = header.PdfFont
        });
      });
      #endregion
      #region Postavljanje izvora podataka i stupaca
      report.MainTableDataSource(dataSource => dataSource.StronglyTypedList(stavke));

      report.MainTableSummarySettings(summarySettings =>
      {
        summarySettings.OverallSummarySettings("Ukupno");
      });

      report.MainTableColumns(columns =>
      {
        #region Stupci po kojima se grupira
        columns.AddColumn(column =>
        {
          column.PropertyName<StavkaDenorm>(s => s.IdDokumenta);
          column.Group(
                    (val1, val2) =>
                    {
                      return (int)val1 == (int)val2;
                    });
        });       
        #endregion
        columns.AddColumn(column =>
        {
          column.IsRowNumber(true);
          column.CellsHorizontalAlignment(HorizontalAlignment.Right);
          column.IsVisible(true);        
          column.Width(1);
          column.HeaderCell("#", horizontalAlignment: HorizontalAlignment.Right);
        });
        columns.AddColumn(column =>
        {
          column.PropertyName<StavkaDenorm>(x => x.NazArtikla);
          column.CellsHorizontalAlignment(HorizontalAlignment.Center);
          column.IsVisible(true);         
          column.Width(4);
          column.HeaderCell("Naziv artikla", horizontalAlignment: HorizontalAlignment.Center);
        });

        columns.AddColumn(column =>
        {
          column.PropertyName<StavkaDenorm>(x => x.KolArtikla);
          column.CellsHorizontalAlignment(HorizontalAlignment.Right);
          column.IsVisible(true);         
          column.Width(1);
          column.HeaderCell("Količina", horizontalAlignment: HorizontalAlignment.Center);
          column.ColumnItemsTemplate(template =>
          {
            template.TextBlock();
            template.DisplayFormatFormula(obj => obj == null || string.IsNullOrEmpty(obj.ToString())
                                                ? string.Empty : string.Format("{0:.00}", obj));
          });
        });

        columns.AddColumn(column =>
        {
          column.PropertyName<StavkaDenorm>(x => x.JedCijArtikla);
          column.CellsHorizontalAlignment(HorizontalAlignment.Right);
          column.IsVisible(true);
          column.Width(1);
          column.HeaderCell("Jedinična cijena", horizontalAlignment: HorizontalAlignment.Center);
          column.ColumnItemsTemplate(template =>
          {
            template.TextBlock();
            template.DisplayFormatFormula(obj => obj == null || string.IsNullOrEmpty(obj.ToString())
                                                ? string.Empty : string.Format("{0:C2}", obj));
          });          
        });

        columns.AddColumn(column =>
        {
          column.PropertyName<StavkaDenorm>(x => x.PostoRabat);
          column.CellsHorizontalAlignment(HorizontalAlignment.Right);
          column.IsVisible(true);
          column.Width(1);
          column.HeaderCell("Rabat", horizontalAlignment: HorizontalAlignment.Center);          
          column.ColumnItemsTemplate(template =>
          {
            template.TextBlock();
            template.DisplayFormatFormula(obj => obj == null || string.IsNullOrEmpty(obj.ToString())
                                                ? string.Empty : string.Format("{0:P2}", obj));
          });
        });

        columns.AddColumn(column =>
        {
          column.PropertyName("Ukupno");
          column.CellsHorizontalAlignment(HorizontalAlignment.Right);
          column.IsVisible(true);         
          column.Width(1);
          column.HeaderCell("Ukupno", horizontalAlignment: HorizontalAlignment.Center);
          column.ColumnItemsTemplate(template =>
          {
            template.TextBlock();
            template.DisplayFormatFormula(obj => obj == null || string.IsNullOrEmpty(obj.ToString())
                                                ? string.Empty : string.Format("{0:C2}", obj));
          });
          column.AggregateFunction(aggregateFunction =>
          {
            aggregateFunction.NumericAggregateFunction(AggregateFunction.Sum);
            aggregateFunction.DisplayFormatFormula(obj => obj == null || string.IsNullOrEmpty(obj.ToString())
                                                ? string.Empty : string.Format("{0:C2}", obj));
          });
          column.CalculatedField(
                        list =>
                        {
                          if (list == null) return string.Empty;
                          decimal kolArtikla = (decimal)list.GetValueOf(nameof(StavkaDenorm.KolArtikla));
                          decimal postoRabat = (decimal)list.GetValueOf(nameof(StavkaDenorm.PostoRabat));
                          decimal jedCijArtikla = (decimal)list.GetValueOf(nameof(StavkaDenorm.JedCijArtikla));
                          var iznos = jedCijArtikla * kolArtikla * (1 - postoRabat);
                          return iznos;
                        });
        });
      });

      #endregion      
      byte[] pdf = report.GenerateAsByteArray();

      if (pdf != null)
      {
        Response.Headers.Add("content-disposition", "inline; filename=artikli.pdf");
        return File(pdf, "application/pdf");
      }
      else
        return NotFound();
    }

    #region Master-detail header
    public class MasterDetailsHeaders : IPageHeader
    {
      private string naslov;
      public MasterDetailsHeaders(string naslov)
      {
        this.naslov = naslov;
      }
      public IPdfFont PdfRptFont { set; get; }

      public PdfGrid RenderingGroupHeader(Document pdfDoc, PdfWriter pdfWriter, IList<CellData> newGroupInfo, IList<SummaryCellData> summaryData)
      {
        var idDokumenta = newGroupInfo.GetSafeStringValueOf(nameof(StavkaDenorm.IdDokumenta));
        var urlDokumenta = newGroupInfo.GetSafeStringValueOf(nameof(StavkaDenorm.UrlDokumenta));        
        var nazPartnera = newGroupInfo.GetSafeStringValueOf(nameof(StavkaDenorm.NazPartnera));
        var datDokumenta = (DateTime) newGroupInfo.GetValueOf(nameof(StavkaDenorm.DatDokumenta));
        var iznosDokumenta = (decimal) newGroupInfo.GetValueOf(nameof(StavkaDenorm.IznosDokumenta));        

        var table = new PdfGrid(relativeWidths: new[] { 2f, 5f, 2f, 3f }) { WidthPercentage = 100 };

        table.AddSimpleRow(
            (cellData, cellProperties) =>
            {
              cellData.Value = "Id dokumenta:";
              cellProperties.PdfFont = PdfRptFont;
              cellProperties.PdfFontStyle = DocumentFontStyle.Bold;
              cellProperties.HorizontalAlignment = HorizontalAlignment.Left;
            },
            (cellData, cellProperties) =>
            {
              cellData.TableRowData = newGroupInfo; //postavi podatke retka za ćeliju
              var cellTemplate = new HyperlinkField(BaseColor.Black, false)
              {
                TextPropertyName = nameof(StavkaDenorm.IdDokumenta),
                NavigationUrlPropertyName = nameof(StavkaDenorm.UrlDokumenta),
                BasicProperties = new CellBasicProperties
                {
                  HorizontalAlignment = HorizontalAlignment.Left,
                  PdfFontStyle = DocumentFontStyle.Bold,
                  PdfFont = PdfRptFont
                }
              };
                         
              cellData.CellTemplate = cellTemplate;              
              cellProperties.PdfFont = PdfRptFont;
              cellProperties.HorizontalAlignment = HorizontalAlignment.Left;
            },            
            (cellData, cellProperties) =>
            {
              cellData.Value = "Datum dokumenta:";
              cellProperties.PdfFont = PdfRptFont;
              cellProperties.PdfFontStyle = DocumentFontStyle.Bold;
              cellProperties.HorizontalAlignment = HorizontalAlignment.Left;
            },
            (cellData, cellProperties) =>
            {
              cellData.Value = datDokumenta;
              cellProperties.PdfFont = PdfRptFont;
              cellProperties.HorizontalAlignment = HorizontalAlignment.Left;
              cellProperties.DisplayFormatFormula = obj => ((DateTime)obj).ToString("dd.MM.yyyy");
            });

        table.AddSimpleRow(
            (cellData, cellProperties) =>
            {
              cellData.Value = "Partner:";
              cellProperties.PdfFont = PdfRptFont;
              cellProperties.PdfFontStyle = DocumentFontStyle.Bold;
              cellProperties.HorizontalAlignment = HorizontalAlignment.Left;
            },
            (cellData, cellProperties) =>
            {
              cellData.Value = nazPartnera;
              cellProperties.PdfFont = PdfRptFont;
              cellProperties.HorizontalAlignment = HorizontalAlignment.Left;
            },
            (cellData, cellProperties) =>
            {
              cellData.Value = "Iznos dokumenta:";
              cellProperties.PdfFont = PdfRptFont;
              cellProperties.PdfFontStyle = DocumentFontStyle.Bold;
              cellProperties.HorizontalAlignment = HorizontalAlignment.Left;
            },
            (cellData, cellProperties) =>
            {
              cellData.Value = iznosDokumenta;
              cellProperties.DisplayFormatFormula = obj => ((decimal)obj).ToString("C2");                                                    
              cellProperties.PdfFont = PdfRptFont;
              cellProperties.HorizontalAlignment = HorizontalAlignment.Left;
            });        
        return table.AddBorderToTable(borderColor: BaseColor.LightGray, spacingBefore: 5f);
      }

      public PdfGrid RenderingReportHeader(Document pdfDoc, PdfWriter pdfWriter, IList<SummaryCellData> summaryData)
      {
        var table = new PdfGrid(numColumns: 1) { WidthPercentage = 100 };
        table.AddSimpleRow(
           (cellData, cellProperties) =>
           {
             cellData.Value = naslov;
             cellProperties.PdfFont = PdfRptFont;
             cellProperties.PdfFontStyle = DocumentFontStyle.Bold;
             cellProperties.HorizontalAlignment = HorizontalAlignment.Center;
           });
        return table.AddBorderToTable();
      }
    }
    #endregion

    #region Private methods
    private PdfReport CreateReport(string naslov)
    {
      var pdf = new PdfReport();

      pdf.DocumentPreferences(doc =>
      {
        doc.Orientation(PageOrientation.Portrait);
        doc.PageSize(PdfPageSize.A4);
        doc.DocumentMetadata(new DocumentMetadata
        {
          Author = "FER-ZPR",
          Application = "Firma.MVC Core",
          Title = naslov
        });
        doc.Compression(new CompressionSettings
        {
          EnableCompression = true,
          EnableFullCompression = true
        });
      })
      .MainTableTemplate(template =>
      {
        template.BasicTemplate(BasicTemplate.ProfessionalTemplate);
      })
      .MainTablePreferences(table =>
      {
        table.ColumnsWidthsType(TableColumnWidthType.Relative);
        //table.NumberOfDataRowsPerPage(20);
        table.GroupsPreferences(new GroupsPreferences
        {
          GroupType = GroupType.HideGroupingColumns,
          RepeatHeaderRowPerGroup = true,
          ShowOneGroupPerPage = true,
          SpacingBeforeAllGroupsSummary = 5f,
          NewGroupAvailableSpacingThreshold = 150,
          SpacingAfterAllGroupsSummary = 5f
        });
        table.SpacingAfter(4f);
      });

      return pdf;
    }
    #endregion
  }
}
