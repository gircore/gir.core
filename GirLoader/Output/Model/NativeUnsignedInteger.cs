namespace GirLoader.Output.Model
{
    public class NativeUnsignedInteger : PrimitiveValueType
    {
        public NativeUnsignedInteger(string ctypeName) : base(new CType(ctypeName), new SymbolName("nuint")) { }
    }
}
