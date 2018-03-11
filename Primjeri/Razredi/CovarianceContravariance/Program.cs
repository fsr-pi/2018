using MotornaVozila;
using System;
using System.Collections.Generic;

namespace CovarianceContravariance
{
  class Program
  {
    static void Main(string[] args)
    {      
      List<Automobil> automobili = new List<Automobil>{
        new Automobil("MMW 1", 120, 3),
        new Automobil("Fiat Tumpo", 60, 5),
        new Automobil("MMW 5", 150, 4)
      };
      IspisiVozila(automobili);

      Motocikl moto1 = new Motocikl("Pljuzuki KRC", 100, false);
      Motocikl moto2 = new Motocikl("Momos APN", 20, true);

      Comparison<MotornoVozilo> comparisonFunction = (a, b) => -a.Snaga.CompareTo(b.Snaga);

      IComparer<MotornoVozilo> comparer = Comparer<MotornoVozilo>.Create(comparisonFunction);
      IspisiBoljiMotor(moto1, moto2, comparer);
      IspisiBoljiMotor(moto1, moto2, comparisonFunction);
    }

    static void IspisiVozila(IEnumerable<MotornoVozilo> vozila)
    {
      foreach (var vozilo in vozila)
      {
        Console.WriteLine("\t " + vozilo.Model);
      }
    }

    static void IspisiBoljiMotor(Motocikl a, Motocikl b, IComparer<Motocikl> comparer)
    {
      Console.WriteLine("\tBolji je " + (comparer.Compare(a, b) < 0 ? a.Model : b.Model));
    }

    static void IspisiBoljiMotor(Motocikl a, Motocikl b, Comparison<Motocikl> comparer)
    {
      Console.WriteLine("\tBolji je " + (comparer(a, b) < 0 ? a.Model : b.Model));
    }
  }
}