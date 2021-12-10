namespace GirLoader.Output
{
    public class UnsignedPointer : PrimitiveType, GirModel.UnsignedPointer
    {
        public UnsignedPointer(string ctype) : base(new CType(ctype)) { }

        internal override bool Matches(TypeReference typeReference)
        {
            if (typeReference.CTypeReference is null)
                return false;

            return typeReference.CTypeReference.CType == CType;
        }
    }
}
