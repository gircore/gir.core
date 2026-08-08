namespace GirLoader.Output;

public class UnpointedSignedByte(string ctype) : SignedByte(ctype)
{
    internal override bool Matches(TypeReference typeReference)
    {
        if (typeReference.CTypeReference?.IsPointer == true)
            return false;

        return base.Matches(typeReference);
    }
}
