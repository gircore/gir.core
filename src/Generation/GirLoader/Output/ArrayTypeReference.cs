namespace GirLoader.Output;

public partial class ArrayTypeReference : TypeReference
{
    public int? Length { get; init; }
    public bool IsZeroTerminated { get; init; }
    public int? FixedSize { get; init; }

    //An array carries exactly one element type ("AnyType" in the GIR schema)
    public TypeReference ElementTypeReference => ElementTypeReferences[0];

    public override Type? Type => ElementTypeReference.Type;

    public ArrayTypeReference(TypeReference elementTypeReference, SymbolNameReference? symbolNameReference, CTypeReference? ctype)
        : base(symbolNameReference, ctype, new[] { elementTypeReference })
    {
    }

    internal override GirModel.AnyType GetResolvedAnyType()
        => GirModel.AnyType.From(this);
}
