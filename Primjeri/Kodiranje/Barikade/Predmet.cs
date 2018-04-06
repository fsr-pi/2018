using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Barikade
{
  class Predmet
  {
    public string Naziv { get; private set; }

    //dictionary je privatna varijabla, a ne svojstvo, 
    //jer bi to omogućilo direktan unos ocjene bez kontrole
    //npr. p.Ocjene["Pero"] = -2;
    //indekser će služiti kao barikada
    private Dictionary<string, int> ocjene = new Dictionary<string, int>();
    public Predmet(string naziv)
    {
      Naziv = naziv;
    }

    public int this[string ime]
    {
      get
      {
        //što ako netko traži studenta koji nije evidentiran?
        //baciti pogrešku, vratiti specijalnu vrijednost? Vidi tehnike obrade pogreške
        bool postoji = ocjene.TryGetValue(ime, out int ocjena);
        return postoji ? ocjena : -1;
      }
      set
      {
        if (value < 1 || value > 5)
        {
          throw new ArgumentOutOfRangeException($"Neispravna ocjena {value}. Ocjena mora biti od 1 do 5");
        }

        ocjene[ime] = value;
      }
    }

    public double ProsjecnaOcjena()
    {      
      int sum = 0;
      foreach(var par in ocjene)
      {
        int ocjena = par.Value;
        Debug.Assert(ocjena >= 1 && ocjena <= 5,
                     $"Pohranjena neispravna ocjena {par.Key} = {par.Value}");
        sum += ocjena;
      }
      return ocjene.Count > 0 ? (double) sum / ocjene.Count : 0;
    }
  }
}
