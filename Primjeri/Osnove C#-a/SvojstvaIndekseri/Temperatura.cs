namespace SvojstvaIndekseri
{
  public class Temperatura
  {
    private float T;
    public float Celsius
    {
      get { return T - 273.16f; }
      set { T = value + 273.16f; }
    }
    public float Fahrenheit
    {
      get { return 9f / 5 * Celsius + 32; }
      set { Celsius = (5f / 9) * (value - 32); }
    }
  }
}