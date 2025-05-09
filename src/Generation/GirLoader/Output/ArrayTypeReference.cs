namespace GirLoader.Output;

public partial class ArrayTypeReference : TypeReference
{
    public int? Length { get; init; }
    public bool IsZeroTerminated { get; init; }
    public int? FixedSize { get; init; }
    public TypeReference TypeReference { get; }

    public override Type? Type => TypeReference.Type;

    public ArrayTypeReference(TypeReference typeReference, SymbolNameReference? symbolNameReference, CTypeReference? ctype) : base(symbolNameReference, ctype)
    {
        TypeReference = typeReference;
    }
}
