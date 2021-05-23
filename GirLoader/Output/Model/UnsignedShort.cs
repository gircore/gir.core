namespace GirLoader.Output.Model
{
    public class UnsignedShort : PrimitiveValueType
    {
        public UnsignedShort(string ctypeName) : base(new CTypeName(ctypeName), new SymbolName("ushort")) { }
    }
}
