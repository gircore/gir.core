namespace GirLoader.Output
{
    public class SignedByte : PrimitiveValueType, GirModel.SignedByte
    {
        public SignedByte(string ctype) : base(new CType(ctype), new TypeName("sbyte")) { }
    }
}
