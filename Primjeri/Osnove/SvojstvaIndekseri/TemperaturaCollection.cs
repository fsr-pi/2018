using System;

namespace SvojstvaIndekseri
{
  public class TemperaturaCollection
  {

    private Temperatura[] nizTemperatura; // polje temperatura
    private int brElemenata; // broj postavljenih
    private int maxBrElemenata; // kapacitet

    public TemperaturaCollection(int MaxBrElemenata)
    {
      nizTemperatura = new Temperatura[MaxBrElemenata];
      maxBrElemenata = MaxBrElemenata;
      brElemenata = 0;
    }

    public void Add(Temperatura t)
    {
      if (brElemenata < maxBrElemenata)
      {
        nizTemperatura[brElemenata] = t;
        brElemenata++;
      }
      else throw new Exception("Pogreška: polje je popunjeno");
    }

    public Temperatura this[int index]   // Indekser
    {
      set
      {
        if (index >= 0 && index < nizTemperatura.Length)
          nizTemperatura[index] = value;
        else throw new Exception("Pogreška: indeks van raspona");
      }
      get
      {
        if (index >= 0 && index < nizTemperatura.Length)
          return nizTemperatura[index];
        else throw new Exception("Pogreška: indeks van raspona");
      }
    }
  }
}
