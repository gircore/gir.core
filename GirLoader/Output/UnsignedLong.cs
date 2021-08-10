namespace GirLoader.Output
{
    public class UnsignedLong : PrimitiveValueType
    {
        public UnsignedLong(string ctype) : base(new CType(ctype), new TypeName("ulong")) { }
    }
}
