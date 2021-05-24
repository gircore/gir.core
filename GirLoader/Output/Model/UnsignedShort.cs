namespace GirLoader.Output.Model
{
    public class UnsignedShort : PrimitiveValueType
    {
        public UnsignedShort(string ctypeName) : base(new CType(ctypeName), new SymbolName("ushort")) { }
    }
}
