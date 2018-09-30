namespace Json5
{
    public class Json5String : Json5Primitive
    {
        private string value;

        public Json5String(string value)
        {
            this.value = value;
        }

        public Json5String(char[] value) : this(new string(value)) { }

        public Json5String(object value) : this(value.ToString()) { }

        public override Json5Type Type
        {
            get { return Json5Type.String; }
        }

        protected override object Value
        {
            get { return this.value; }
        }

        internal override string ToJson5String(string space, string indent, bool useOneSpaceIndent = false)
        {
            return AddIndent(Json5.QuoteString(this.value), indent, useOneSpaceIndent);
        }

        public static implicit operator string(Json5String value)
        {
            return value.value;
        }
    }
}
