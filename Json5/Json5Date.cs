using System;

namespace Json5
{
    public class Json5Date : Json5Primitive
    {
        private DateTimeOffset value;

        public Json5Date(DateTimeOffset value)
        {
            this.value = value;
        }

        public override Json5Type Type
        {
            get { return Json5Type.Date; }
        }

        protected override object Value
        {
            get { return this.value; }
        }

        internal override string ToJson5String(string space, string indent, bool useOneSpaceIndent = false)
        {
            return AddIndent(Json5.QuoteString(this.value.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'fff'Z'")), indent, useOneSpaceIndent);
        }

        public static implicit operator DateTimeOffset(Json5Date value)
        {
            return value.value;
        }

        public static implicit operator DateTime(Json5Date value)
        {
            return value.value.DateTime;
        }
    }
}
