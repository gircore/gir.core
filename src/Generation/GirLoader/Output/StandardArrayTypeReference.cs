namespace GirLoader.Output;

public class StandardArrayTypeReference : ArrayTypeReference, GirModel.StandardArrayType
{
    public StandardArrayTypeReference(ArrayTypeReference arrayTypeReference) : base(
        elementTypeReference: arrayTypeReference.ElementTypeReference,
        symbolNameReference: arrayTypeReference.SymbolNameReference,
        ctype: arrayTypeReference.CTypeReference)
    {
        Length = arrayTypeReference.Length;
        FixedSize = arrayTypeReference.FixedSize;
        IsZeroTerminated = arrayTypeReference.IsZeroTerminated;
    }
}
