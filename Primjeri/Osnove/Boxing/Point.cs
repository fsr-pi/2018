namespace Boxing
{
  struct Point
  {
    public int cx;
    public int cy;

    public Point(int x, int y)
    {
      cx = x;
      cy = y;
    }

    public override string ToString() 
    {
      return "(" + cx + ", " + cy + ")";
    }
  }
}
