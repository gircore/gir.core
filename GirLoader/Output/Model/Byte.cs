namespace GirLoader.Output.Model
{
    public class Byte : PrimitiveValueType
    {
        public Byte(string ctypeName) : base(new CType(ctypeName), new SymbolName("byte")) { }
    }
}
