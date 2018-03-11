using System;

namespace MotornaVozila
{
  // razred Automobil (izveden iz razreda MotornoVozilo)
  public class Automobil : MotornoVozilo
  {
    // dodatni atribut
    public int BrVrata { get; set; }


    // konstruktor
    public Automobil(string model, double snaga, int brVrata)
      : base(model, snaga)  // poziv baznog konstruktora
    {
      Console.WriteLine("Nastaje auto " + model);
      this.BrVrata = brVrata;
    }

    // nadjačavanje bazne metode Start() 
    public override void Start()
    {
      Console.WriteLine("Kreće auto " + Model);
    }

    // dodatna metoda za Automobil
    public void VeziPojas()
    {
      Console.WriteLine("Auto: Veži pojas!");
    }

  }
}
