using System.Collections.Generic;

namespace Json5
{
    using Parsing;
    using System.Text.RegularExpressions;

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

        // https://www.ecma-international.org/ecma-262/5.1/
        // Match IdentifierName (except escapes)
        private static Regex identifierNameRegex = new Regex(@"
            ^
                [\$_\p{L}\p{Nl}]
                [\$_\p{L}\p{Nl}\p{Mn}\p{Mc}\p{Nd}\p{Pc}\u200c\u200d]*
            $
        ", RegexOptions.IgnorePatternWhitespace | RegexOptions.Compiled);

        private string KeyToString(string key)
        {
            if (identifierNameRegex.IsMatch(key))
                return key;

            return Json5.QuoteString(key);
        }
    }
}
