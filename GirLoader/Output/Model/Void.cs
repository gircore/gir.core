namespace GirLoader.Output.Model
{
    public class Void : PrimitiveType
    {
        public Void(string nativeName) : base(new CType(nativeName), new SymbolName("void")) { }
    }
}
