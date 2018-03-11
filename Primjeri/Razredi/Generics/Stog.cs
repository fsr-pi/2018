using System;
using System.Text;

namespace Generics
{
   // vlastiti genericki razred
  public class Stog<T>
  {
    readonly int maxVelicina;
    int vrh = 0;
    T[] elementi; // kojeg je tipa T ?
    public Stog()
      : this(100) // default konstruktor poziva onaj s argumentom
    {
      // prazno tijelo
    }

    public Stog(int velicina) // konstruktor s argumentom
    {
      maxVelicina = velicina;
      elementi = new T[maxVelicina];
    }

    public void Stavi(T element)
    {
      if (vrh >= maxVelicina)
        throw new InvalidOperationException("Stog je pun");
      elementi[vrh] = element;
      vrh++;
      // isto što i elementi[vrh++] = element;
    }

    public T Skini()
    {
      vrh--;
      if (vrh >= 0)
      {
        return elementi[vrh];
      }
      else
      {
        vrh = 0;
        throw new InvalidOperationException("Stog je prazan!");
      }
    }

    public override string ToString()
    {
      StringBuilder sb = new StringBuilder();
      for (int i = vrh - 1; i >= 0; i--)
      {
       sb.Append(elementi[i].ToString());
       if (i > 0) sb.Append(", ");
      }
      return sb.ToString();
    }
  }


  
}
