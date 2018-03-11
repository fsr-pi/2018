using System;


namespace Postupci
{
  public class Postupak_v2//modifikacija razreda Postupak. Izbačeni dio koji nije bitan za demonstraciju svojstava
  {
    // učahurivanje (encapsulation) - skrivanje člana
    private int brOp = 0;

    public int Zbroji(int x, int y)
    {
      ++brOp;
      return x + y;
    }

    public int Oduzmi(int x, int y)
    {
      ++brOp;
      return x - y;
    }

    //umjesto postupka za pristup skrivenom članu koristi se tzv. Svojstvo 
    public int BrOp
    {
      get //omogućava dohvat vrijednosti člana
      {
        return brOp;
      }
      //set //omogućava promjenu vrijednosti
      //{
      //    brOp = value;
      //}
    }
  }
}