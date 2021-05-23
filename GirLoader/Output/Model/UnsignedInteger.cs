namespace GirLoader.Output.Model
{
    public class UnsignedInteger : PrimitiveValueType
    {
        public UnsignedInteger(string ctypeName) : base(new CTypeName(ctypeName), new SymbolName("uint")) { }
    }
}
