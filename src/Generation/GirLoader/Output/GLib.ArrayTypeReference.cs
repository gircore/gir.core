namespace GirLoader.Output;

public class GLibArrayTypeReference : ArrayTypeReference, GirModel.GLibArrayType
{
    public GLibArrayTypeReference(ArrayTypeReference arrayTypeReference) : base(
            typeReference: arrayTypeReference.TypeReference,
            symbolNameReference: arrayTypeReference.SymbolNameReference,
            ctype: arrayTypeReference.CTypeReference)
    {
        Length = arrayTypeReference.Length;
        FixedSize = arrayTypeReference.FixedSize;
        IsZeroTerminated = false; //Can't be zero terminated as it is a special GLib struct
    }
}
