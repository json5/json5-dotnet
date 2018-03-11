using System;

namespace Json5
{
    public abstract class Json5Value
    {
        public static readonly Json5Null Null = new Json5Null();

        public abstract Json5Type Type { get; }

        public virtual Json5Value this[string key]
        {
            get { throw new NotSupportedException(); }
            set { throw new NotSupportedException(); }
        }

        public virtual Json5Value this[int index]
        {
            get { throw new NotSupportedException(); }
            set { throw new NotSupportedException(); }
        }

        public override string ToString()
        {
            return this.ToJson5String();
        }

        public string ToJson5String(string space = null)
        {
            if (space != null && space.Length > 10)
                space = space.Remove(10);

            return this.ToJson5String(space, "");
        }

        internal abstract string ToJson5String(string space, string indent);

        public static implicit operator Json5Value(string value)
        {
            if (value == null)
                return null;

            return new Json5String(value);
        }

        public static implicit operator Json5Value(double value)
        {
            return new Json5Number(value);
        }

        public static implicit operator Json5Value(double? value)
        {
            if (value.HasValue)
                return new Json5Number(value.Value);

            return null;
        }

        public static implicit operator Json5Value(bool value)
        {
            return new Json5Boolean(value);
        }

        public static implicit operator Json5Value(bool? value)
        {
            if (value.HasValue)
                return new Json5Boolean(value.Value);

            return null;
        }

        public static explicit operator string(Json5Value value)
        {
            return (Json5String)value;
        }

        public static explicit operator double(Json5Value value)
        {
            return (Json5Number)value;
        }

        public static explicit operator double? (Json5Value value)
        {
            return (Json5Number)value;
        }

        public static explicit operator bool(Json5Value value)
        {
            return (Json5Boolean)value;
        }

        public static explicit operator bool? (Json5Value value)
        {
            return (Json5Boolean)value;
        }
    }
}
