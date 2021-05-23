namespace GirLoader.Output.Model
{
    public class Pointer : PrimitiveType
    {
        public Pointer(string ctypeName) : base(new CTypeName(ctypeName), new SymbolName("IntPtr")) { }
    }
}
