namespace Json5
{
    public class Json5UnexpectedEndOfInputException : Json5ParsingException
    {
        internal Json5UnexpectedEndOfInputException(int line, int column) :
          base(string.Format("Unexpected end of input at line {0} column {1}", line, column), line, column)
        { }
    }
}
