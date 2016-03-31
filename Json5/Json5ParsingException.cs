using System;
using System.Collections.Generic;
using System.Linq;

namespace Json5
{
  public class Json5ParsingException : Exception
  {
    internal Json5ParsingException(string message, int line, int column) : base(message)
    {
      this.Line = line;
      this.Column = column;
    }

    internal Json5ParsingException(Exception innerException, int line, int column) : base(innerException.Message, innerException)
    {
      this.Line = line;
      this.Column = column;
    }

    public int Line { get; }

    public int Column { get; }
  }
}
