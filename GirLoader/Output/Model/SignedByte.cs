namespace GirLoader.Output.Model
{
    public class SignedByte : PrimitiveValueType
    {
        public SignedByte(string ctypeName) : base(new CTypeName(ctypeName), new SymbolName("sbyte")) { }
    }
}
