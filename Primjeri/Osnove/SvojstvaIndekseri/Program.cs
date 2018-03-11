using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SvojstvaIndekseri
{
  public class Program
  {
    public static void Main(string[] args)
    {
      Temperatura X = new Temperatura(); // step into
      X.Fahrenheit = 70;
      Console.WriteLine("{0} = {1}", X.Fahrenheit, X.Celsius);
      X.Celsius = 36.5f;
      Console.WriteLine("{0} = {1}", X.Fahrenheit, X.Celsius);


      Temperatura Y = new Temperatura(); // step over
      Y.Fahrenheit = 60;

      TemperaturaCollection DnevneTemperature = new TemperaturaCollection(3);
      DnevneTemperature.Add(X);
      DnevneTemperature.Add(Y);

      // set i get indeksera

      DnevneTemperature[2] = X;

      Console.WriteLine("Dnevna t0: {0}", DnevneTemperature[0].Celsius);
      Console.WriteLine("Dnevna t1: {0}", DnevneTemperature[1].Celsius);
      Console.WriteLine("Dnevna t2: {0}", DnevneTemperature[2].Celsius);
    }
  }
}
