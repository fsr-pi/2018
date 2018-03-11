using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Barikade
{
  public class Pitagora
  {
    public double C
    {
      get
      {
        return Korijen(A * A + B * B);
      }
    }

    private double a;

    public double A //javno svojstvo (sluzi kao barikada)
    {
      get
      {
        return a;
      }
      set
      {
        //provjeravamo je li unutar dozvoljenih vrijednosti
        //ako nije, pridjeljujemo joj najblizu dozvoljenu vrijednost ili neku drugu pretpostavljenu
        if (value <= 0)
        {
          Console.WriteLine($"{value} mora biti veci od 0, postavljam na 1");
          a = 1;
        }
        else
        {
          a = value;
        }
      }
    }

    public double B { get; private set; }

    public void SetBFromString(string s) //javna metoda (sluzi kao barikada)
    {
      try
      {
        double x = double.Parse(s);
        if (x <= 0)
        {
          Console.WriteLine($"{s} mora biti veci od 0, postavljam na 1");
          x = 1;
        }
      }
      catch (Exception exc)
      {
        Console.WriteLine($"{s} nije broj -> postavljam na 1 ({exc.Message})");
        B = 1;
      }
    }

    public double Korijen(double broj) //u principu ovo bi bio privatni postupak, ali demonstriramo Assert
    {
      Debug.Assert(broj >= 0, "Broj mora biti nenegativan");
      double korijen = Math.Sqrt(broj);
      return korijen;
    }   
  }
}
