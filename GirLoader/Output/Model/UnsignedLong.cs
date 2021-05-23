namespace GirLoader.Output.Model
{
    public class UnsignedLong : PrimitiveValueType
    {
        public UnsignedLong(string ctypeName) : base(new CTypeName(ctypeName), new SymbolName("ulong")) { }
    }
}
