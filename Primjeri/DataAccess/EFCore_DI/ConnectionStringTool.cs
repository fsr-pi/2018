using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace EFCore_DI
{
  public class ConnectionStringTool : IConnectionStringTool
  {
    private string connectionString;
    public ConnectionStringTool()
    {
      Console.WriteLine("---- Ucitavam appsettings.json");
      var config = new ConfigurationBuilder()
                      .AddUserSecrets("Firma")
                      .SetBasePath(Directory.GetCurrentDirectory())
                      .AddJsonFile("appsettings.json")
                      .Build();
      connectionString = config["ConnectionStrings:Firma"];
      connectionString = connectionString.Replace("sifra", config["FirmaSqlPassword"]);      
    }
    public string GetConnectionString()
    {      
      return connectionString;
    }
  }
}
