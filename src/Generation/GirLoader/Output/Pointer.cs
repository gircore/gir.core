namespace GirLoader.Output
{
    public class Pointer : PrimitiveType, GirModel.Pointer
    {
        public Pointer(string ctype) : base(new CType(ctype)) { }

        internal override bool Matches(TypeReference typeReference)
        {
            if (typeReference.CTypeReference is null)
                return false;

            return typeReference.CTypeReference.CType == CType;
        }
    }
}
