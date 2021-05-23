namespace GirLoader.Output.Model
{
    public class UnsignedPointer : PrimitiveType
    {
        public UnsignedPointer(string cTypeName) : base(new CTypeName(cTypeName), new SymbolName("UIntPtr")) { }
    }
}
