namespace GirLoader.Output.Model
{
    public class Integer : PrimitiveValueType
    {
        public Integer(string ctypeName) : base(new CType(ctypeName), new SymbolName("int")) { }
    }
}
