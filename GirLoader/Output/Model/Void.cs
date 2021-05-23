namespace GirLoader.Output.Model
{
    public class Void : PrimitiveType
    {
        public Void(string nativeName) : base(new CTypeName(nativeName), new SymbolName("void")) { }
    }
}
