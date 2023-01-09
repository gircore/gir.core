namespace GirLoader.Output;

public class StandardArrayTypeReference : ArrayTypeReference, GirModel.StandardArrayType
{
    public StandardArrayTypeReference(ArrayTypeReference arrayTypeReference) : base(
        typeReference: arrayTypeReference.TypeReference,
        symbolNameReference: arrayTypeReference.SymbolNameReference,
        ctype: arrayTypeReference.CTypeReference)
    {
        Length = arrayTypeReference.Length;
        FixedSize = arrayTypeReference.FixedSize;
        IsZeroTerminated = arrayTypeReference.IsZeroTerminated;
    }
}
