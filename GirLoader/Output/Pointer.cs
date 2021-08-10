namespace GirLoader.Output
{
    public class Pointer : PrimitiveType
    {
        public Pointer(string ctype) : base(new CType(ctype), new TypeName("IntPtr")) { }

        internal override bool Matches(TypeReference typeReference)
        {
            if (typeReference.CTypeReference is null)
                return false;

            return typeReference.CTypeReference.CType == CType;
        }
    }
}
