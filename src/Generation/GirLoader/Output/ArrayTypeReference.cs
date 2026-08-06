namespace GirLoader.Output;

public partial class ArrayTypeReference : TypeIdentifier
{
    public int? Length { get; init; }
    public bool IsZeroTerminated { get; init; }
    public int? FixedSize { get; init; }
    public CTypeReference? CTypeReference { get; }
    public SymbolNameReference? SymbolNameReference { get; }
    public AnyTypeReference AnyTypeReference { get; }

    public ArrayTypeReference(AnyTypeReference anyTypeReference, SymbolNameReference? symbolNameReference, CTypeReference? ctype)
    {
        CTypeReference = ctype;
        SymbolNameReference = symbolNameReference;
        AnyTypeReference = anyTypeReference;
    }
}
