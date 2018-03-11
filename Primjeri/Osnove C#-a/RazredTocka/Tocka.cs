namespace RazredTocka
{
  class Tocka
  {
    //varijable
    public int x;
    public int y;
   
    //konstruktor
    public Tocka(int x, int y)
    {
      this.x = x;
      this.y = y;
    }

    //javna metoda
    public int Kvadrant()
    {
      if (x > 0)
      {
        if (y > 0) return 1;
        else return 4;
      }
      else
      {
        if (y > 0) return 2;
        else return 3;
      }
    }
  }
}