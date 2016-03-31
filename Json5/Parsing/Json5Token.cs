using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Json5.Parsing
{
  [DebuggerDisplay("Type = {Type}, Value = {Value}, Line = {Line}, Column = {Column}")]
  class Json5Token
  {
    public Json5TokenType Type { get; set; }

    public object Value { get; set; }

    private string input;
    public string Input
    {
      get
      {
        if(this.input == null && this.Value != null)
          return this.Value.ToString();

        return this.input;
      }

      set
      {
        this.input = value;
      }
    }

    public int Line { get; set; }

    public int Column { get; set; }

    public string String
    {
      get { return (string)this.Value; }
    }

    public char Character
    {
      get { return (char)this.Value; }
    }

    public double Number
    {
      get { return (double)this.Value; }
    }
  }
}
