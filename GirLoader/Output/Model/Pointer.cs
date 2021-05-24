namespace GirLoader.Output.Model
{
    public class Pointer : PrimitiveType
    {
        public Pointer(string ctypeName) : base(new CType(ctypeName), new SymbolName("IntPtr")) { }
    }
}
