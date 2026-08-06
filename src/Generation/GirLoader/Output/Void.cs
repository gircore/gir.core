namespace GirLoader.Output;

public class Void(string ctype) : Type(ctype), GirModel.Void
{
    internal override bool Matches(TypeReference typeReference)
    {
        if (typeReference.CTypeReference is null)
            return false;

        return typeReference.CTypeReference.CType == CType
               && !typeReference.CTypeReference.IsPointer;
    }
}
