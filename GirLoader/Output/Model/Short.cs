namespace GirLoader.Output.Model
{
    public class Short : PrimitiveValueType
    {
        public Short(string nativeName) : base(new CTypeName(nativeName), new SymbolName("short")) { }
    }
}
