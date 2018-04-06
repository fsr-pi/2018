using Microsoft.Extensions.Configuration;
using System;
using System.Data.Common;
using System.Data.SqlClient;
using System.IO;

namespace Procedure
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

      int prag = int.Parse(config["Prag"]);
      PrimjerPozivaProcedure(connString, prag);
    }

    private static void PrimjerPozivaProcedure(string connString, int prag)
    {
      DbProviderFactory factory = SqlClientFactory.Instance;
      using (DbConnection conn = factory.CreateConnection())
      {
        conn.ConnectionString = connString;
        using (DbCommand command = factory.CreateCommand())
        {
          command.Connection = conn;
          command.CommandText = "ap_ArtikliSkupljiOd";
          command.CommandType = System.Data.CommandType.StoredProcedure;

          DbParameter param = command.CreateParameter();
          param.ParameterName = "Prag";
          param.DbType = System.Data.DbType.Decimal;
          param.Value = prag;
          command.Parameters.Add(param);

          param = command.CreateParameter();
          param.ParameterName = "BrojSkupljih";
          param.DbType = System.Data.DbType.Int32;
          param.Direction = System.Data.ParameterDirection.Output;

          command.Parameters.Add(param);

          param = command.CreateParameter();
          param.ParameterName = "BrojJeftinijih";
          param.DbType = System.Data.DbType.Int32;
          param.Direction = System.Data.ParameterDirection.Output;
          command.Parameters.Add(param);

          conn.Open();
          using (DbDataReader reader = command.ExecuteReader())
          {
            Console.WriteLine("Artikli skuplji od praga ({0})", prag);
            Console.WriteLine("---------------------------------");
            while (reader.Read())
            {
              Console.WriteLine("\t{0} - {1}", reader["NazArtikla"], reader["CijArtikla"]);
            }
            Console.WriteLine("---------");
          }
          //gotovi s readerom -> uzmimo parametre
          Console.WriteLine("Broj skupljih: {0}, broj jeftinijih: {1}",
              command.Parameters["BrojSkupljih"].Value, command.Parameters["BrojJeftinijih"].Value);
        }
      }
    }
  }
}