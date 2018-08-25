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

        internal override string ToJson5String(string space, string indent)
        {
            string newLine = string.IsNullOrEmpty(space) ? "" : "\n";

            // TODO: Use string builder instead of string
            string s = "{" + newLine;

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
                s += (property.Value ?? Null).ToJson5String(space, indent + space);
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
                return key;

            return Json5.QuoteString(key);
        }
    }
}
