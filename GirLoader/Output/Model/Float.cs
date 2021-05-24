namespace GirLoader.Output.Model
{
    public class Float : PrimitiveValueType
    {
        public Float(string ctypeName) : base(new CType(ctypeName), new SymbolName("float")) { }
    }
}
