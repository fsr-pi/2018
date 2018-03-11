using System;
using System.Collections.Generic;
using System.Text;

namespace CustomException
{

  class Iznimka : Exception
  {
    private string val;

    public Iznimka()
      : base()
    {
    }

    public Iznimka(string str)
      : base(str)
    {
      val = str;
    }
    public override string Message
    {
      get
      {
        return "Nije neparan " + val;
      }
    }
  }

}
