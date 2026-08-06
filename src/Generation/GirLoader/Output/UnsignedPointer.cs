namespace GirLoader.Output;

public class UnsignedPointer(string ctype) : Type(ctype), GirModel.UnsignedPointer
{
    internal override bool Matches(TypeReference typeReference)
    {
        if (typeReference.CTypeReference is null)
            return false;

        return typeReference.CTypeReference.CType == CType;
    }
}
