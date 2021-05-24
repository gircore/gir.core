namespace GirLoader.Output.Model
{
    public class Short : PrimitiveValueType
    {
        public Short(string nativeName) : base(new CType(nativeName), new SymbolName("short")) { }
    }
}
