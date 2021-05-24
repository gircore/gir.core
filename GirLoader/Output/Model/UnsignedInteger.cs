namespace GirLoader.Output.Model
{
    public class UnsignedInteger : PrimitiveValueType
    {
        public UnsignedInteger(string ctypeName) : base(new CType(ctypeName), new SymbolName("uint")) { }
    }
}
