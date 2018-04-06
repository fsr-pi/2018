using Microsoft.Extensions.Configuration;
using System;
using System.Data.Common;
using System.Data.SqlClient;
using System.IO;

namespace Parametri
{
  class Program
  {

    private static string GetConnectionString()
    {
      var config = new ConfigurationBuilder()
                      .AddUserSecrets("Firma")
                      .SetBasePath(Directory.GetCurrentDirectory())
                      .AddJsonFile("appsettings.json")
                      .Build();
      string connString = config["ConnectionStrings:Firma"];
      connString = connString.Replace("sifra", config["FirmaSqlPassword"]);
      return connString;
    }

    static void Main(string[] args)
    {
      string connString = GetConnectionString();
      ParametriziraniUpit(connString);
    }

    /// <summary>
    /// Primjer stvaranja priključka na bazu korištenjem razreda DBProviderFactory
    /// Upit se postavlja korištenjem parametara i vraća 2 skupa rezultata
    /// </summary>
    private static void ParametriziraniUpit(string connString)
    {
      //DbProviderFactory factory = DbProviderFactories.GetFactory(factoryName); //još nije u .net core-u
      DbProviderFactory factory = SqlClientFactory.Instance;

      using (DbConnection conn = factory.CreateConnection())
      {
        conn.ConnectionString = connString;
        using (DbCommand command = factory.CreateCommand())
        {
          command.Connection = conn;
          command.CommandText = "SELECT TOP 3 * FROM Artikl WHERE JedMjere = @JedMjere ORDER BY CijArtikla DESC;" +
            "SELECT TOP 3 * FROM Artikl WHERE JedMjere = @JedMjere AND CijArtikla > @Cijena ORDER BY CijArtikla";
          DbParameter param = command.CreateParameter();
          param.ParameterName = "JedMjere";
          param.DbType = System.Data.DbType.String;
          param.Value = "kom";
          command.Parameters.Add(param);

          param = factory.CreateParameter();
          param.ParameterName = "Cijena";
          param.DbType = System.Data.DbType.Decimal;
          param.Value = 100m;
          command.Parameters.Add(param);
          conn.Open();
          using (DbDataReader reader = command.ExecuteReader())
          {
            bool prviProlaz = true;
            do
            {
              Console.WriteLine(prviProlaz ? "Najskuplji artikli po komadu:" : "Najjeftiniji artikli po komadu (ali skuplji od 100)");
              Console.WriteLine("-----------------------------");
              while (reader.Read())
              {
                Console.WriteLine("\t{0} - {1}", reader["NazArtikla"], reader["CijArtikla"]);
              }
              Console.WriteLine();
              prviProlaz = false;
            }
            while (reader.NextResult());
          }
        }
      }
    }
  }
}