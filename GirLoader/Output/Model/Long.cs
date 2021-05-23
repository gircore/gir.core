namespace GirLoader.Output.Model
{
    public class Long : PrimitiveValueType
    {
        public Long(string ctypeName) : base(new CTypeName(ctypeName), new SymbolName("long")) { }
    }
}
