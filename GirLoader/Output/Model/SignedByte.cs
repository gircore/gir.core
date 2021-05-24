namespace GirLoader.Output.Model
{
    public class SignedByte : PrimitiveValueType
    {
        public SignedByte(string ctypeName) : base(new CType(ctypeName), new SymbolName("sbyte")) { }
    }
}
