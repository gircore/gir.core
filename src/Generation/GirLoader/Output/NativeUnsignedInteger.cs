namespace GirLoader.Output
{
    public class NativeUnsignedInteger : PrimitiveValueType
    {
        public NativeUnsignedInteger(string ctype) : base(new CType(ctype), new TypeName("nuint")) { }
    }
}
