using System;

namespace MotornaVozila
{
  // razred Motocikl (izveden iz razreda MotornoVozilo)
  public class Motocikl : MotornoVozilo
  {
    // dodatni atribut
    public bool Prikolica { get; set; }


    // konstruktor
    public Motocikl(string model, double snaga, bool prikolica)
      : base(model, snaga)  // poziv baznog konstruktora
    {
      Console.WriteLine("Nastaje motor " + model);
      this.Prikolica = prikolica;
    }

    // nadjačavanje bazne metode 
    public override void Start()
    {
      // ... motor kreće drukčije od automobila
      Console.WriteLine("Kreće motor " + Model);
    }

  }
}
