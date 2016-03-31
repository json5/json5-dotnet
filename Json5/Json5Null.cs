using System;
using System.Collections.Generic;
using System.Linq;

namespace Json5
{
  public class Json5Null : Json5Value
  {
    internal Json5Null() { }

    public override Json5Type Type
    {
      get { return Json5Type.Null; }
    }

    internal override string ToJson5String(string space, string indent)
    {
      return Json5.QuoteString("null");
    }
  }
}
