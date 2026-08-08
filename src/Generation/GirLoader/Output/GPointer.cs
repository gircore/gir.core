namespace GirLoader.Output;

public class GPointer() : Pointer("gpointer")
{
    internal override bool Matches(TypeReference typeReference)
    {
        if (typeReference.CTypeReference is null)
            return false;

        return typeReference.CTypeReference.CType == CType && typeReference.SymbolNameReference?.SymbolName == "gpointer";
    }
}
