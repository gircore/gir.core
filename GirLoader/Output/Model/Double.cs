namespace GirLoader.Output.Model
{
    public class Double : PrimitiveValueType
    {
        public Double(string ctypeName) : base(new CTypeName(ctypeName), new SymbolName("double")) { }
    }
}
