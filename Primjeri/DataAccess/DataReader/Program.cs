using Microsoft.Extensions.Configuration;
using System;
using System.Data.SqlClient;
using System.IO;

namespace DataReader
{
  class Program
  {
    static void Main(string[] args)
    {
      var config = new ConfigurationBuilder()
                            .AddUserSecrets("Firma")
                            .SetBasePath(Directory.GetCurrentDirectory())
                            .AddJsonFile("appsettings.json")
                            .Build();
      string connString = config["ConnectionStrings:Firma"];
      connString = connString.Replace("sifra", config["FirmaSqlPassword"]);

      try
      {
        using (var conn = new SqlConnection(connString))
        {
          using (var command = conn.CreateCommand())
          {
            command.CommandText = "SELECT TOP 3 * FROM Artikl";
            command.Connection = conn;

            conn.Open();

            using (var reader = command.ExecuteReader())
            {
              while (reader.Read())
              {
                object SifArtikla = reader["SifArtikla"];
                object NazArtikla = reader["NazArtikla"];
                object Usluga = reader["ZastUsluga"];
                Console.WriteLine("{0} - {1}  {2}", SifArtikla.ToString(), NazArtikla.ToString(), (bool)Usluga ? "(Usluga)" : "");
              }
            }
          }
        }
      }
      catch (Exception exc)
      {
        Console.WriteLine("Pogreška: " + exc.Message + exc.StackTrace);
      }
    }    
  }
}