namespace GirLoader.Output;

public class GLibByteArrayTypeReference : ArrayTypeReference, GirModel.GLibByteArrayType
{
    public GLibByteArrayTypeReference(ArrayTypeReference arrayTypeReference) : base(
        anyTypeReference: arrayTypeReference.AnyTypeReference,
        symbolNameReference: arrayTypeReference.SymbolNameReference,
        ctype: arrayTypeReference.CTypeReference)
    {
        Length = arrayTypeReference.Length;
        FixedSize = arrayTypeReference.FixedSize;
        IsZeroTerminated = false; //Can't be zero terminated as it is a special GLib struct
    }
}
