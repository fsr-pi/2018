using System;


namespace Postupci
{
  public class Postupak_v3//modifikacija razred Postupak_v3, tako da koristi automatsko svojstvo
  {
    //Za trivijalne slučajeve korištenja get i set u svojstvima može se olakšati pisanje korištenjem automatskog svojstva
    //interno se kreira privatna varijabla
    public int BrOp { get; private set; } = 0;

    public int Zbroji(int x, int y)
    {
      ++BrOp;
      return x + y;
    }

    public int Oduzmi(int x, int y)
    {
      ++BrOp;
      return x - y;
    }
  }
}