using Firma.Mvc.ViewModels;
using System;
using Xunit;

namespace Firma.Mvc.Tests
{
  public class DocumentFilterTests
  {
    [Fact]
    [Trait("UnitTest", "DocumentFilter")]
    public void PrazniFilter()
    {
      string filterString = "-----";
      DokumentFilter filter = DokumentFilter.FromString(filterString);
      Assert.True(filter.IsEmpty());
    }

    [Fact]
    [Trait("UnitTest", "DocumentFilter")]
    public void DobroFormiranString()
    {
      DokumentFilter filter = new DokumentFilter();
      filter.IdPartnera = 1;
      filter.IznosOd = 300;
      filter.IznosDo = 500;
      filter.NazPartnera = "Nebitno";
      filter.DatumOd = new DateTime(1911, 2, 13, 9, 15, 0);
      filter.DatumDo = new DateTime(2011, 2, 13, 20, 15, 0);
      string filterString = filter.ToString();
      string expected = "1-13.02.1911-13.02.2011-300-500";
      Assert.Equal(expected, filterString);
    }
  }
}
