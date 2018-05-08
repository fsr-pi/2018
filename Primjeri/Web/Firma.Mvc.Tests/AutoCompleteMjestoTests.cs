using Firma.Mvc.Controllers.AutoComplete;
using Firma.Mvc.Models;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Firma.Mvc.Tests
{
  public class AutoCompleteMjestoTests
  {
    FirmaContext ctx;
    IOptionsSnapshot<AppSettings> options;
    public AutoCompleteMjestoTests()
    {
      //Arrange
      var builder = new ConfigurationBuilder()
          .AddUserSecrets("Firma")
          .AddJsonFile("appsettings.json");        
      var Configuration = builder.Build();

      var appSection = Configuration.GetSection("AppSettings");

      string connectionString = appSection.GetValue<string>("ConnectionString");
      connectionString = connectionString.Replace("sifra", Configuration["FirmaSqlPassword"]);

      var dbContextBuilder = new DbContextOptionsBuilder<FirmaContext>()
                       .UseSqlServer(connectionString);

      ctx = new FirmaContext(dbContextBuilder.Options);

      //imitacija sučelja IOptionsSnapshot<AppSettings>
      var mockOptions = new Mock<IOptionsSnapshot<AppSettings>>();
      var appSettings = new AppSettings
      {
        AutoCompleteCount = appSection.GetValue<int>("AutoCompleteCount") //ostali argumenti su nebitni za ovu grupu testova
      };
      mockOptions.SetupGet(appsettings => appsettings.Value).Returns(appSettings);
      options = mockOptions.Object;      
    }

    [Trait("UnitTest", "AutoComplete")]    
    [Theory]
    [InlineData("varaždin")]
    [InlineData("VARAŽDIN")]
    [InlineData("araž")]
    [InlineData("ždin")]
    public void PronalaziZeljenoMjesto(string value)
    {
      //Act
      var controller = new Controllers.AutoComplete.MjestoController(ctx, options);
      IEnumerable<IdLabel> result = controller.Get(value);

      //Assert
      string naziv = "Varaždin";
      var containsValue = result.Select(idlabel => idlabel.Label)
                                .Any(label => label.IndexOf(naziv, StringComparison.CurrentCultureIgnoreCase) != -1);

      Assert.True(containsValue);

      string nazivKojegNema = "Split";

      containsValue = result.Select(idlabel => idlabel.Label)
                                .Any(label => label.IndexOf(nazivKojegNema, StringComparison.CurrentCultureIgnoreCase) != -1);

      Assert.False(containsValue);
    }

    [Trait("UnitTest", "AutoComplete")]
    [Theory]
    [InlineData("a", 20)]
    [InlineData("šđžć", 0)]
    [InlineData("a", 50)]
    public void VracaOgranicenSkupRezultata(string value,int count)
    {
      options.Value.AutoCompleteCount = count; //pazi! može utjecati na ostale testove
      //Act
      var controller = new Controllers.AutoComplete.MjestoController(ctx, options);
      IEnumerable<IdLabel> result = controller.Get(value);

      //Assert
      result.Should().HaveCount(count, $"because there should at least {count} places containing text {value}");
    }
  }
}
