using System;

namespace MotornaVozila
{
  // razred MotornoVozilo (bazni razred)
  public class MotornoVozilo
  {
    // atributi
    public string Model { get; set; }
    public double Snaga { get; set; }


    // konstruktor osnovnog razreda
    public MotornoVozilo(string model, double snaga)
    {
      Console.WriteLine("Nastaje vozilo " + model);
      this.Model = model;
      this.Snaga = snaga;
    }

    // metoda (zajednicka svim vozilima)
    public void DajGas()
    {
      Console.WriteLine("Vozilo " + Model + " daje gas!");
    }

    // postupak koji izvedeni razredi implementiraju zasebno
    public virtual void Start()
    {
      Console.WriteLine("Start vozila " + Model);
    }

  }
}






