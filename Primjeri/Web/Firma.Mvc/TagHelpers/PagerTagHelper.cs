using Firma.Mvc.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.Extensions.Options;
using System;
using System.Linq;

namespace Firma.Mvc.TagHelpers
{
  /// <summary>
  /// Tag helper za kreiranje vlastitih poveznica na stranice u rezultatu nekog upravljača
  /// Upotrebljava se kao atribut HTML oznake *pager* koju mijenja u div
  /// <example>
  /// Primjer upotrebe
  /// ```
  /// <pager page-info="@Model.PagingInfo" page-action="Index" page-title="Unesite željenu stranicu" class="float-right">
  /// </pager>
  /// ```
  /// U datoteku *_ViewImports.cshtml* potrebno dodati `@addTagHelper *, Microsoft.AspNetCore.Mvc.TagHelpers`  
  /// te u pogled uključiti vlastitu javascript datoteku *gotopage.js*
  /// </example>
  /// </summary>  
  [HtmlTargetElement(Attributes="page-info")]
  public class PagerTagHelper : TagHelper
  {

    private readonly IUrlHelperFactory urlHelperFactory;  
    private readonly AppSettings appData;   
    public PagerTagHelper(IUrlHelperFactory helperFactory, IOptionsSnapshot<AppSettings> options)
    {      
      urlHelperFactory = helperFactory;
      appData = options.Value;
    }

    [ViewContext]
    [HtmlAttributeNotBound]
    public ViewContext ViewContext { get; set; }

    /// <summary>
    /// Serijalizirani string koji sadrži informacije o trenutnoj i ukupnom broju stranicu 
    /// </summary>
    public PagingInfo PageInfo { get; set; }

    /// <summary>
    /// Serijalizirani string kojim se prenose informacije o aktivnom filtriranju podataka
    /// </summary>
    public IPageFilter PageFilter { get; set; }

    /// <summary>
    /// Akcija na koju poveznica treba voditi
    /// </summary>
    public string PageAction { get; set; }

    /// <summary>
    /// Tekst za tooltip za trenutni broj stranice i unos ciljane stranice
    /// </summary>
    public string PageTitle { get; set; }

    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
      output.TagName = "div";
      int offset = appData.PageOffset;
      TagBuilder navTag = new TagBuilder("nav");
      TagBuilder paginationList = new TagBuilder("ul");
      paginationList.AddCssClass("pagination");
      navTag.InnerHtml.AppendHtml(paginationList);

      if (PageInfo.CurrentPage - offset > 1)
      {
        var tag = BuildTagForPage(1, "1..");
        paginationList.InnerHtml.AppendHtml(tag);
      }
      for (int i = Math.Max(1, PageInfo.CurrentPage - offset);
               i <= Math.Min(PageInfo.TotalPages, PageInfo.CurrentPage + offset);
               i++)
      {
        if (i != PageInfo.CurrentPage)
        {
          var tag = BuildTagForPage(i);
          paginationList.InnerHtml.AppendHtml(tag);
        }
        else
        {
          var tag = BuildPageInputTag(i.ToString());
          paginationList.InnerHtml.AppendHtml(tag);
        }
      }

      if (PageInfo.CurrentPage + offset < PageInfo.TotalPages)
      {
        var tag = BuildTagForPage(PageInfo.TotalPages, ".. " + PageInfo.TotalPages);
        paginationList.InnerHtml.AppendHtml(tag);
      }

      output.Content.AppendHtml(navTag);     
    }


    /// <summary>
    /// Stvara polje za prikaz trenutne stranice i unos željene stranice
    /// </summary>
    /// <param name="text">Broj trenutne stranice</param>
    /// <returns></returns>
    private TagBuilder BuildPageInputTag(string text)
    {
      IUrlHelper urlHelper = urlHelperFactory.GetUrlHelper(ViewContext);
      TagBuilder tag = new TagBuilder("input");      
      tag.Attributes["type"] = "text";      
      tag.Attributes["value"] = text;
      tag.Attributes["data-current"] = text;
      tag.Attributes["data-min"] = "1";
      tag.Attributes["data-max"] = PageInfo.TotalPages.ToString();
      tag.Attributes["data-url"] = urlHelper.Action(PageAction, new
      {
        page = -1,
        sort = PageInfo.Sort,
        ascending = PageInfo.Ascending,
        filter = PageFilter?.ToString()
      });     
      tag.AddCssClass("pagebox");//da ga se može pronaći i stilizirati

      if (!string.IsNullOrWhiteSpace(PageTitle))
      {
        tag.Attributes["title"] = PageTitle;
      }

      TagBuilder listItemTag = new TagBuilder("li");
      listItemTag.AddCssClass("page-item active");
      listItemTag.InnerHtml.AppendHtml(tag);

      return tag;
    }

    /// <summary>
    /// Stvara oznaku za i-tu stranicu koristeći *i* kao sadržaj poveznice
    /// <seealso cref="BuildTagForPage(int, string)"/>
    /// </summary>
    /// <param name="i">broj stranice</param>
    /// <returns>TagBuilder s pripremljenom poveznicom</returns>
    private TagBuilder BuildTagForPage(int i)
    {
      return BuildTagForPage(i, i.ToString());
    }

    /// <summary>
    ///  Stvara oznaku za i-tu stranicu koristeći argument text kao sadržaj poveznice
    /// </summary>
    /// <param name="i">broj stranice</param>
    /// <param name="text">tekst poveznice</param>
    /// <returns>TagBuilder s pripremljenom poveznicom</returns>
    private TagBuilder BuildTagForPage(int i, string text)
    {
      IUrlHelper urlHelper = urlHelperFactory.GetUrlHelper(ViewContext);   
      TagBuilder tag = new TagBuilder("a");
      tag.InnerHtml.Append(text);
      tag.Attributes["href"] = urlHelper.Action(PageAction, new
      {
        page = i,
        sort = PageInfo.Sort,
        ascending = PageInfo.Ascending,
        filter = PageFilter?.ToString()
      });
      tag.AddCssClass("page-link");      

      TagBuilder listItemTag = new TagBuilder("li");
      listItemTag.AddCssClass("page-item");
      listItemTag.InnerHtml.AppendHtml(tag);

      return listItemTag;
    }
  }
}
