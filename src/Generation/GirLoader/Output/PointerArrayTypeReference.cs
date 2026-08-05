namespace GirLoader.Output;

public class PointerArrayTypeReference : ArrayTypeReference, GirModel.PointerArrayType
{
    public PointerArrayTypeReference(ArrayTypeReference arrayTypeReference) : base(
        elementTypeReference: arrayTypeReference.ElementTypeReference,
        symbolNameReference: arrayTypeReference.SymbolNameReference,
        ctype: arrayTypeReference.CTypeReference)
    {
        Length = arrayTypeReference.Length;
        FixedSize = arrayTypeReference.FixedSize;
        IsZeroTerminated = false; //Can't be zero terminated as it is a special GLib struct
    }
}
