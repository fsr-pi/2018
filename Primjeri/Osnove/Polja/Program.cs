using System;

namespace Polja
{
  public class Program
  {
    public static void Main(string[] args)
    {      
      Polja();
      PravokutnaPolja();
      NazubljenaPolja();
    }

    private static void Polja()
    {
      int[] a = new int[10];
      int[] b = new int[] { 1, 2, 3 };
      int[] c = { 4, 5, 6, 7, 8, 9 };
      int[] d = c;
      int[] e = new int[a.Length];

      Console.WriteLine("Polje kao niz određene duljine");
      for (int i = 0; i < b.Length; i++)
      {
        Console.Write(b[i] + " ");
        a[i] = b[i];
      }

      Console.WriteLine("\nPolje kao kolekcija objekata");
      foreach (int br in d)
      {
        Console.Write(br + " ");
      }
      Console.WriteLine();

      int[] z = new int[] { 3, 1, 2 };
      System.Array.Sort(z);

      Console.WriteLine("Tražim 3, indeks= {0}", Array.BinarySearch(z, 3));
      Console.WriteLine("Tražim 5, indeks= {0}", Array.BinarySearch(z, 5));

      Array o = new int[3];
      Array.Copy(z, o, 2);
      Array.Reverse(o);
    }

    private static void PravokutnaPolja()
    {
      int[,] a = new int[3, 5]; //inicijalno vrijednosti 0

      int[,] b = new int[2, 3] { { 1, 2, 3 }, { 4, 5, 6 } };
      //može i ovako:
      //int[,] b = new int[,] { { 1, 2, 3 }, { 4, 5, 6 } };

      int[,] c = { { 1, 2, 3 }, { 4, 5, 6 } };

      int[,,,] d = new int[5, 6, 7, 8];

      for (int i = 0; i < 2; i++)
      {
        for (int j = 0; j < 3; j++)
        {
          Console.Write(b[i, j]);
          Console.Write(' ');
        }
        Console.WriteLine();
      }
    }

    private static void NazubljenaPolja()
    {
      int[][] a = new int[][] { new int[] { 2, 3, 4 }, new int[] { 5, 6, 7, 8, 9 } };
      int[][] b = { new int[] { 2, 3, 4 }, new int[] { 5, 6, 7, 8 } };

      for (int i = 0; i < b.Length; i++)
      {
        for (int j = 0; j < b[i].Length; j++)
        {
          Console.Write(b[i][j]);
          Console.Write(' ');
        }
        Console.WriteLine();
      }

      int[][] c;
      c = new int[5][]; c[0] = new int[3]; c[1] = new int[7];
      for (int i = 0; i < c.Length; i++)
      {
        if (c[i] != null)
        {
          for (int j = 0; j < c[i].Length; j++)
          {
            Console.Write(c[i][j]);
            Console.Write(' ');
          }
          Console.WriteLine();
        }
      }
    }
  }
}
