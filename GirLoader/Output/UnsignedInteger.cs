namespace GirLoader.Output
{
    public class UnsignedInteger : PrimitiveValueType
    {
        public UnsignedInteger(string ctype) : base(new CType(ctype), new TypeName("uint")) { }
    }
}
