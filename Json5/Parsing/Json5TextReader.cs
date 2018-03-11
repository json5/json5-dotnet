using System.IO;

namespace Json5.Parsing
{
    class Json5TextReader
    {
        private TextReader reader;

        public Json5TextReader(TextReader reader)
        {
            this.reader = reader;
        }

        public int Line { get; private set; } = 1;

        public int Column { get; private set; } = 1;

        public int Read()
        {
            int r = this.reader.Read();

            if (r == '\n')
            {
                this.Line++;
                this.Column = 1;
            }
            else if (r >= 0)
                this.Column++;

            return r;
        }

        public int Peek()
        {
            return this.reader.Peek();
        }
    }
}
