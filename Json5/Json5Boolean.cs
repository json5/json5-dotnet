using System;
using System.Collections.Generic;
using System.Linq;

namespace Json5
{
  public class Json5Boolean : Json5Primitive
  {
    private bool value;

    public Json5Boolean(bool value)
    {
      this.value = value;
    }

    public override Json5Type Type
    {
      get { return Json5Type.Boolean; }
    }

    protected override object Value
    {
      get { return this.value; }
    }

    internal override string ToJson5String(string space, string indent)
    {
      return Json5.QuoteString(this.value.ToString().ToLower());
    }

    public static implicit operator bool(Json5Boolean value)
    {
      return value.value;
    }
  }
}
