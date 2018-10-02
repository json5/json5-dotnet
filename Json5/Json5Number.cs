using System.Globalization;

namespace Json5
{
    public class Json5Number : Json5Primitive
    {
        private double value;

        public Json5Number(double value)
        {
            this.value = value;
        }

        public override Json5Type Type
        {
            get { return Json5Type.Number; }
        }

        protected override object Value
        {
            get { return this.value; }
        }

        internal override string ToJson5String(string space, string indent, bool useOneSpaceIndent = false)
        {
            return AddIndent(this.value.ToString(CultureInfo.InvariantCulture), indent, useOneSpaceIndent);
        }

        public static implicit operator double(Json5Number value)
        {
            return value.value;
        }
    }
}
