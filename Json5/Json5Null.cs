namespace Json5
{
    public class Json5Null : Json5Value
    {
        internal Json5Null() { }

        public override Json5Type Type
        {
            get { return Json5Type.Null; }
        }

        internal override string ToJson5String(string space, string indent)
        {
            return SpaceHandler.AddSpace("null", space);
        }
    }
}
