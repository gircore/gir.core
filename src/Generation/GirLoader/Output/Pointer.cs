namespace GirLoader.Output;

public class Pointer(string ctype) : Type(ctype), GirModel.Pointer
{
    internal override bool Matches(TypeReference typeReference)
    {
        if (typeReference.CTypeReference is null)
            return false;

        return typeReference.CTypeReference.CType == CType;
    }
}
