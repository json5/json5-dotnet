using System.Collections.Generic;

namespace Json5
{
    using Parsing;

    public class Json5Object : Json5Container, IEnumerable<KeyValuePair<string, Json5Value>>
    {
        private Dictionary<string, Json5Value> dictionary = new Dictionary<string, Json5Value>();

        public override void Add(string key, Json5Value value)
        {
            this.dictionary.Add(key, value);
        }

        public override bool ContainsKey(string key)
        {
            return this.dictionary.ContainsKey(key);
        }

        public override ICollection<string> Keys
        {
            get { return this.dictionary.Keys; }
        }

        public override bool Remove(string key)
        {
            return this.dictionary.Remove(key);
        }

        public override ICollection<Json5Value> Values
        {
            get { return this.dictionary.Values; }
        }

        public override Json5Value this[string key]
        {
            get { return this.dictionary[key]; }
            set { this.dictionary[key] = value; }
        }

        public override Json5Value this[int index]
        {
            get { return this[index.ToString()]; }
            set { base[index.ToString()] = value; }
        }

        public override void Clear()
        {
            this.dictionary.Clear();
        }

        public override int Count
        {
            get { return this.dictionary.Count; }
        }

        public IEnumerator<KeyValuePair<string, Json5Value>> GetEnumerator()
        {
            return this.dictionary.GetEnumerator();
        }

        public override Json5Type Type
        {
            get { return Json5Type.Object; }
        }

        internal override string ToJson5String(string space, string indent, bool useOneSpaceIndent = false)
        {
            // "If white space is used, trailing commas will be used in objects and arrays." from specification
            bool forcedCommaAndNewLineRequired = !string.IsNullOrEmpty(space);

            string newLine = string.IsNullOrEmpty(space) ? "" : "\n";

            string currentIndent = useOneSpaceIndent ? " " : indent;

            // TODO: Use string builder instead of string
            string s = currentIndent + "{" + newLine;

            bool isFirstValue = true;

            foreach (var property in this)
            {
                if (isFirstValue)
                {
                    isFirstValue = false;
                }
                else
                {
                    s += "," + newLine;
                }

                s += indent + space + KeyToString(property.Key) + ":";

                s += (property.Value ?? Null).ToJson5String(space, indent + space, forcedCommaAndNewLineRequired);
            }

            if (forcedCommaAndNewLineRequired)
            {
                s += "," + newLine;
            }

            s += indent + "}";

            return s;
        }

        private string KeyToString(string key)
        {
            if (key.Length == 0)
                return "''";


            // TODO: Create a Utility class for interally used methods.

            //if(char.IsLetter(key[0]) || char.GetUnicodeCategory(key[0]) == System.Globalization.UnicodeCategory.LetterNumber)
            //{
            //  for(int i = 1; i < key.Length; i++)
            //  {

            //  }
            //}

            // This will not always work unless we check for Eof after the Identifier.
            // We should probably handle this another way.
            if (new Json5Lexer(key).Read().Type == Json5TokenType.Identifier)
            {
                if (DoesIdentifierNeedDoubleQuotes(key))
                {
                    return Json5.QuoteString(key);
                }

                return key;
            }

            return Json5.QuoteString(key);
        }

        /// <summary>
        /// Does the identifier need extra double quotes, Currently used to check if there would be issue with single quotes
        /// </summary>
        /// <param name="input">String to check</param>
        /// <returns>True if needs; False otherwise</returns>
        private static bool DoesIdentifierNeedDoubleQuotes(string input)
        {
            // If input contains single quotes that do not start and end the input, then it needs double quotes
            if (input.Contains("'"))
            {
                // If it starts and ends with single quote, everything is OK
                if (input[0] == '\'' && input[input.Length - 1] == '\'')
                {
                    return false;
                }

                // Otherwise it need double quotes
                return true;
            }

            return false;
        }
    }
}
