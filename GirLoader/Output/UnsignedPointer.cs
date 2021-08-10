namespace GirLoader.Output
{
    public class UnsignedPointer : PrimitiveType
    {
        public UnsignedPointer(string ctype) : base(new CType(ctype), new TypeName("UIntPtr")) { }

        internal override bool Matches(TypeReference typeReference)
        {
            if (typeReference.CTypeReference is null)
                return false;

            return typeReference.CTypeReference.CType == CType;
        }
    }
}
