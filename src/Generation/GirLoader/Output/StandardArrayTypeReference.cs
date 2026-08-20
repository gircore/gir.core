namespace GirLoader.Output;

public class StandardArrayTypeReference : ArrayTypeReference, GirModel.StandardArrayTypeReference
{
    public StandardArrayTypeReference(ArrayTypeReference arrayTypeReference) : base(
        anyTypeReference: arrayTypeReference.AnyTypeReference,
        symbolNameReference: arrayTypeReference.SymbolNameReference,
        ctype: arrayTypeReference.CTypeReference)
    {
        Length = arrayTypeReference.Length;
        FixedSize = arrayTypeReference.FixedSize;
        IsZeroTerminated = arrayTypeReference.IsZeroTerminated;
    }
}
