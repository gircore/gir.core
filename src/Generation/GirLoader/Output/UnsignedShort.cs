namespace GirLoader.Output
{
    public class UnsignedShort : PrimitiveValueType, GirModel.UnsignedShort
    {
        public UnsignedShort(string ctype) : base(new CType(ctype)) { }
    }
}
