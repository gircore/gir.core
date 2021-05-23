namespace GirLoader.Output.Model
{
    public class NativeUnsignedInteger : PrimitiveValueType
    {
        public NativeUnsignedInteger(string ctypeName) : base(new CTypeName(ctypeName), new SymbolName("nuint")) { }
    }
}
