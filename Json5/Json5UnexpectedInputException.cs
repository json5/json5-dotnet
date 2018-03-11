namespace Json5
{
    public class Json5UnexpectedInputException : Json5ParsingException
    {
        internal Json5UnexpectedInputException(string input, int line, int column) :
          base(string.Format("Unexpected input '{0}' at line {1} column {2}", input, line, column), line, column)
        { }
    }
}
