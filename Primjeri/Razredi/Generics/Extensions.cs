using System.Collections.Generic;


namespace Generics
{
  public static class Extensions
  {
    public static V DohvatiIliStvori<K, V>(this Dictionary<K, V> dict, K key) where V : new()
    {
      if (!dict.ContainsKey(key))
      {
        V val = new V();
        dict[key] = val;
      }
      return dict[key];        
    }
  }
}
