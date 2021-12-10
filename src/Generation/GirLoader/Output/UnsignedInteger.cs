namespace GirLoader.Output
{
    public class UnsignedInteger : PrimitiveValueType, GirModel.UnsignedInteger
    {
        public UnsignedInteger(string ctype) : base(new CType(ctype)) { }
    }
}
