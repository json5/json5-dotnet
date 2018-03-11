namespace Json5
{
    public abstract class Json5Primitive : Json5Value
    {
        protected abstract object Value { get; }

        public override int GetHashCode()
        {
            return this.Value.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            Json5Primitive o = obj as Json5Primitive;
            return (object)o == null ? false : o.Value == this.Value;
        }

        public static bool operator ==(Json5Primitive a, Json5Primitive b)
        {
            return a.Value == b.Value;
        }

        public static bool operator !=(Json5Primitive a, Json5Primitive b)
        {
            return !(a == b);
        }
    }
}
