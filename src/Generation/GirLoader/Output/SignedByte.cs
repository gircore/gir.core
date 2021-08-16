namespace GirLoader.Output
{
    public class SignedByte : PrimitiveValueType
    {
        public SignedByte(string ctype) : base(new CType(ctype), new TypeName("sbyte")) { }
    }
}
