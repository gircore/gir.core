using System.Collections.Generic;

namespace GirLoader.Output;

public class ResolveableTypeReference : TypeReference
{
    private Type? _resolvedType;
    public override Type? Type => _resolvedType;

    public ResolveableTypeReference(SymbolNameReference? symbolNameReference, CTypeReference? ctype, IReadOnlyList<TypeReference>? elementTypeReferences = null)
        : base(symbolNameReference, ctype, elementTypeReferences)
    {
    }

    public void ResolveAs(Type type)
    {
        _resolvedType = type;
    }

    internal override GirModel.AnyType GetResolvedAnyType()
        => GirModel.AnyType.From(GetResolvedType());
}
