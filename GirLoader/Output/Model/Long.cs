namespace GirLoader.Output.Model
{
    public class Long : PrimitiveValueType
    {
        public Long(string ctypeName) : base(new CType(ctypeName), new SymbolName("long")) { }
    }
}
