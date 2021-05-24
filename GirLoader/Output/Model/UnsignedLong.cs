namespace GirLoader.Output.Model
{
    public class UnsignedLong : PrimitiveValueType
    {
        public UnsignedLong(string ctypeName) : base(new CType(ctypeName), new SymbolName("ulong")) { }
    }
}
