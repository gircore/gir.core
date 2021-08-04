namespace GirLoader.Output.Model
{
    public class UnpointedSignedByte : SignedByte
    {
        public UnpointedSignedByte(string ctype) : base(ctype) { }

        internal override bool Matches(TypeReference typeReference)
        {
            if (typeReference.CTypeReference?.IsPointer == true)
                return false;

            return base.Matches(typeReference);
        }
    }
}
