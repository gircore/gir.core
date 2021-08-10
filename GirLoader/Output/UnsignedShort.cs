namespace GirLoader.Output
{
    public class UnsignedShort : PrimitiveValueType
    {
        public UnsignedShort(string ctype) : base(new CType(ctype), new TypeName("ushort")) { }
    }
}
