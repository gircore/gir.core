namespace GirLoader.Output.Model
{
    public class Boolean : PrimitiveValueType
    {
        public Boolean(string ctypeName) : base(new CType(ctypeName), new SymbolName("bool")) { }
    }
}
