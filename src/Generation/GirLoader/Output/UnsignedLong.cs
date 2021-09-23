namespace GirLoader.Output
{
    public class UnsignedLong : PrimitiveValueType, GirModel.UnsignedLong
    {
        public UnsignedLong(string ctype) : base(new CType(ctype), new TypeName("ulong")) { }
    }
}
