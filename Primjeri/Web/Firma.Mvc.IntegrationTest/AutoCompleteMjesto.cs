using Firma.Mvc.Controllers.AutoComplete;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Runtime.Serialization.Json;
using System.Threading.Tasks;
using Xunit;


namespace Firma.Mvc.IntegrationTest
{
  public class AutoCompleteMjesto
  {
    private readonly TestServer server;
    private readonly HttpClient client;
    private const string addressPrefix = "AutoComplete/Mjesto?term=";
    public AutoCompleteMjesto()
    {
      // Arrange
      server = new TestServer(new WebHostBuilder()
          .UseStartup<Startup>());
      client = server.CreateClient();
    }

    [Theory]
    [Trait("IntegrationTest", "AutoCompleteMjesto")]
    [InlineData("čapljina", 88300)]
    [InlineData("ČAPLJINA", 88300)]

    public async Task VracaSamoJedanGrad(string naziv, int pbr)
    {
      // Act
      var response = await client.GetAsync(addressPrefix + naziv);
      response.EnsureSuccessStatusCode();

      var stream = await response.Content.ReadAsStreamAsync();     
      var serializer = new DataContractJsonSerializer(typeof(IEnumerable<IdLabel>));
      var gradovi = serializer.ReadObject(stream) as IEnumerable<IdLabel>;

      Assert.NotEmpty(gradovi);
      Assert.Single(gradovi);
      Assert.Equal(pbr + " " + naziv, gradovi.First().Label, ignoreCase: true);
      Assert.NotEqual(default(int), gradovi.First().Id);
    }

    [Theory]
    [Trait("IntegrationTest", "AutoCompleteMjesto")]
    [InlineData("Nepostojeći grad")]
    public async Task VracaPrazanPopisGradova(string naziv)
    {
      // Act
      var response = await client.GetAsync(addressPrefix + naziv);
      response.EnsureSuccessStatusCode();

      var stream = await response.Content.ReadAsStreamAsync();
      var serializer = new DataContractJsonSerializer(typeof(IEnumerable<IdLabel>));
      var gradovi = serializer.ReadObject(stream) as IEnumerable<IdLabel>;

      Assert.Empty(gradovi);      
    }

    [Theory]
    [Trait("IntegrationTest", "AutoCompleteMjesto")]        
    [InlineData("ara")]
    [InlineData("din")]
    [InlineData("VARAŽDIN")]
    [InlineData("Varaždin")]

    public async Task VracaViseGradova(string naziv)
    {
      // Act
      var response = await client.GetAsync(addressPrefix + naziv);
      response.EnsureSuccessStatusCode();

      var stream = await response.Content.ReadAsStreamAsync();
      var serializer = new DataContractJsonSerializer(typeof(IEnumerable<IdLabel>));
      var gradovi = serializer.ReadObject(stream) as IEnumerable<IdLabel>;

      Assert.NotEmpty(gradovi);
      Assert.True(gradovi.Count() > 1);

      foreach (var grad in gradovi)
      {
        Assert.Contains(naziv, grad.Label, StringComparison.CurrentCultureIgnoreCase);
        Assert.NotEqual(default(int), grad.Id);
      }
    }
  }
}
