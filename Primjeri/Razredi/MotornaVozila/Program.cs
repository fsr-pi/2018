using System;

namespace MotornaVozila
{
  class Program
  {
    static void Main(string[] args)
    {                                                                              
      MotornoVozilo v = new MotornoVozilo("BWM XP SP1", 133);
      v.Start();

      // objekt tipa Automobil
      Automobil auto = new Automobil("Fiat Tumpo", 100, 5);
      Console.WriteLine("auto.model = " + auto.Model);
      Console.WriteLine("auto.snaga = " + auto.Snaga);
      Console.WriteLine("auto.brVrata = " + auto.BrVrata);

      auto.VeziPojas();
      auto.Start();
      auto.DajGas();

      ((MotornoVozilo)auto).Start();
      ((MotornoVozilo)auto).DajGas();

      // objekt tipa Motocikl
      Motocikl moto = new Motocikl("Pljuzuki KRC", 100, false);
      Console.WriteLine("moto.model = " + moto.Model);
      Console.WriteLine("moto.snaga = " + moto.Snaga);
      Console.WriteLine("moto.prikolica = " + moto.Prikolica);

      moto.Start();
      moto.DajGas();

      ((MotornoVozilo)moto).Start();
      ((MotornoVozilo)moto).DajGas();
    }
  }
}