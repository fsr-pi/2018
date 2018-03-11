using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PreopterecenjeOperatora
{
  public class ComplexNumber
  {
    public int Re { get; set; }
    public int Im { get; set; }

    public ComplexNumber() { }
    public ComplexNumber(int a, int b)
    {
      Re = a;
      Im = b;
    }

    public override string ToString()
    {
      return "( " + Re +
        (Im < 0 ? " - " + (Im * -1) :
        " + " + Im) + "i )";
    }

    // overload the addition operator
    // note the static modifier
    public static ComplexNumber operator +(ComplexNumber x, ComplexNumber y)
    {
      // prođe kroz gettere argumenata i konstruktor rezultata
      return new ComplexNumber(x.Re + y.Re, x.Im + y.Im);
    }

    // provide alternative to overloaded + operator
    public static ComplexNumber Add(ComplexNumber x, ComplexNumber y)
    {
      // prođe kroz operator 
      return x + y;
    }

    // overload the subtraction operator
    public static ComplexNumber operator -(ComplexNumber x, ComplexNumber y)
    {
      return new ComplexNumber(x.Re - y.Re, x.Im - y.Im);
    }

    // provide alternative to overloaded - operator
    public static ComplexNumber Subtract(ComplexNumber x, ComplexNumber y)
    {
      return x - y;
    }

    // overload the multiplication operator
    public static ComplexNumber operator *(ComplexNumber x, ComplexNumber y)
    {
      return new ComplexNumber(x.Re * y.Re - x.Im * y.Im, x.Re * y.Im + y.Re * x.Im);
    }

    // provide alternative to overloaded * operator
    public static ComplexNumber Multiply(ComplexNumber x, ComplexNumber y)
    {
      return x * y;
    }

  }

  class Program
  {
    static void Main(string[] args)
    {
      ComplexNumber x = new ComplexNumber(10, 20);
      ComplexNumber y = new ComplexNumber(30, 40);

      Console.WriteLine(x + " + " + y + " = " + (x + y));
      Console.WriteLine(x + " - " + y + " = " + (x - y));
      Console.WriteLine(x + " * " + y + " = " + (x * y));

      Console.WriteLine(x + " + " + y + " = " + ComplexNumber.Add(x, y));
      Console.WriteLine(x + " - " + y + " = " + ComplexNumber.Subtract(x, y));
      Console.WriteLine(x + " * " + y + " = " + ComplexNumber.Multiply(x, y));

    }
  }
}
