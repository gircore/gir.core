namespace GirLoader.Output;

public class VoidPointer() : Pointer("void")
{
    internal override bool Matches(TypeReference typeReference)
    {
        if (typeReference.CTypeReference is null)
            return false;

        return typeReference.CTypeReference.CType == CType
               && typeReference.CTypeReference.IsPointer;
    }
}
