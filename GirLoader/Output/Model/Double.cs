namespace GirLoader.Output.Model
{
    public class Double : PrimitiveValueType
    {
        public Double(string ctypeName) : base(new CType(ctypeName), new SymbolName("double")) { }
    }
}
