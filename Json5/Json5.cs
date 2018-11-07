using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace Json5
{
    using Parsing;

    /// <summary>
    /// Contains methods for parsing and generating JSON5 text.
    /// </summary>
    public static class Json5
    {
        /// <summary>
        /// Parses JSON5 text into a JSON5 value.
        /// </summary>
        /// <param name="text">The text to parse.</param>
        /// <param name="reviver">An optional function to tranform the parsed values.</param>
        /// <returns>A JSON5 value.</returns>
        public static Json5Value Parse(string text, Func<Json5Container, string, Json5Value, Json5Value> reviver = null)
        {
            Json5Parser parser = new Json5Parser(new StringReader(text));
            Json5Value value = parser.Parse();

            if (reviver != null)
                return Transform(value, reviver);

            return value;
        }

        public static Json5Value Parse(string text, Func<string, Json5Value, Json5Value> reviver)
        {
            throw new NotImplementedException();
        }

        public static string Stringify(Json5Value value, Func<Json5Container, string, Json5Value, Json5Value> replacer, string space = null)
        {
            if (replacer != null)
                value = Transform(value, replacer);

            return value.ToJson5String(space);
        }

        public static string Stringify(Json5Value value, Func<Json5Container, string, Json5Value, Json5Value> replacer, int space)
        {
            return Stringify(value, replacer, new string(' ', Math.Min(space, 10)));
        }

        public static string Stringify(Json5Value value, Func<string, Json5Value, Json5Value> replacer, string space = null)
        {
            Func<Json5Container, string, Json5Value, Json5Value> finalReplacer = null;
            if (replacer != null)
                finalReplacer = (t, k, v) => replacer(k, v);

            return Stringify(value, finalReplacer, space);
        }

        public static string Stringify(Json5Value value, Func<string, Json5Value, Json5Value> replacer, int space)
        {
            throw new NotImplementedException();
        }

        public static string Stringify(Json5Value value, IEnumerable<string> keys, string space = null)
        {
            Func<Json5Container, string, Json5Value, Json5Value> replacer = null;
            if (keys != null)
                replacer = (t, k, v) => keys.Contains(k) ? v : null;

            return Stringify(value, replacer, space);
        }

        public static string Stringify(Json5Value value, IEnumerable<string> keys, int space)
        {
            return Stringify(value, keys, new string(' ', Math.Max(space, 10)));
        }

        public static string Stringify(Json5Value value, string space = null)
        {
            return Stringify(value, (Func<Json5Container, string, Json5Value, Json5Value>)null, space);
        }

        public static string Stringify(Json5Value value, int space)
        {
            return Stringify(value, (Func<Json5Container, string, Json5Value, Json5Value>)null, space);
        }

        //public static string Stringify(Json5Value value)
        //{
        //    return Stringify(value, replacer: null, space: null);
        //}

        private static Func<Json5Container, string, Json5Value, Json5Value> GetKeyTransformer(IEnumerable<string> keys)
        {
            return (t, k, v) => keys.Contains(k) ? v : null;
        }

        private static Json5Value Transform(Json5Value value, Func<Json5Container, string, Json5Value, Json5Value> transformer)
        {
            Json5Object holder = new Json5Object();
            holder[""] = value;
            return Walk(holder, "", transformer);
        }

        private static Json5Value Walk(Json5Container holder, string key, Func<Json5Container, string, Json5Value, Json5Value> transformer)
        {
            Json5Value value = holder[key];
            if (value is Json5Container)
            {
                Json5Container c = (Json5Container)value;
                string[] keys = c.Keys.ToArray();
                foreach (string k in keys)
                {
                    Json5Value v = Walk(c, k, transformer);
                    if (v != null)
                        c[k] = v;
                    else
                        c.Remove(k);
                }
            }

            // Special case for holder
            if (key == "")
            {
                return value;
            }

            return transformer(holder, key, value);
        }

        internal static string QuoteString(string s)
        {
            int doubleQuotes = 0;
            int singleQuotes = 0;
            foreach (char c in s)
            {
                if (c == '"')
                    doubleQuotes++;
                else if (c == '\'')
                    singleQuotes++;
            }

            char quote = doubleQuotes >= singleQuotes ? '\'' : '"';
            return quote + EscapeString(s, quote) + quote;
        }

        internal static string EscapeString(string s, char quote)
        {
            string r = "";
            foreach (char c in s)
                r += EscapeChar(c, quote);

            return r;
        }

        private static string EscapeChar(char c, char quote)
        {
            if (c == quote)
                return "\\" + quote;

            switch (c)
            {
                case '\b': return "\\b";
                case '\t': return "\\t";
                case '\n': return "\\n";
                case '\f': return "\\f";
                case '\r': return "\\r";
                case '\\': return "\\\\";
                case '\u2028': return "\\u2028";
                case '\u2029': return "\\u2029";
            }

            switch (char.GetUnicodeCategory(c))
            {
                case UnicodeCategory.Control:
                case UnicodeCategory.Format:
                case UnicodeCategory.Surrogate:
                case UnicodeCategory.PrivateUse:
                case UnicodeCategory.OtherNotAssigned:
                    return "\\u" + ((int)c).ToString("x4");
            }

            // // Node does this.
            //if(c <= 31)
            //  return "\\u" + ((int)c).ToString("x4");

            return c.ToString();
        }
    }
}
