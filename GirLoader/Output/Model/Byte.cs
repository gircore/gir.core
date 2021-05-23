namespace GirLoader.Output.Model
{
    public class Byte : PrimitiveValueType
    {
        public Byte(string ctypeName) : base(new CTypeName(ctypeName), new SymbolName("byte")) { }
    }
}
