namespace GirLoader.Output
{
    public class Pointer : Type, GirModel.Pointer
    {
        public Pointer(string ctype) : base(ctype) { }

        internal override bool Matches(TypeReference typeReference)
        {
            if (typeReference.CTypeReference is null)
                return false;

            return typeReference.CTypeReference.CType == CType;
        }
    }
}
