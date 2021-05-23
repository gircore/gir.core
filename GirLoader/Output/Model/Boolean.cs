namespace GirLoader.Output.Model
{
    public class Boolean : PrimitiveValueType
    {
        public Boolean(string ctypeName) : base(new CTypeName(ctypeName), new SymbolName("bool")) { }
    }
}
