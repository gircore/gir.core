using System;
using System.Collections.Generic;

namespace GirLoader.Output;

public partial class TypeReference : TypeIdentifier
{
    public CTypeReference? CTypeReference { get; }
    public SymbolNameReference? SymbolNameReference { get; }
    public Type? Type { get; private set; }
    public IReadOnlyList<AnyTypeReference> ElementTypeReferences { get; internal set; }

    public TypeReference(SymbolNameReference? symbolNameReference, CTypeReference? ctypeReference, IReadOnlyList<AnyTypeReference> elementTypeReferences)
    {
        CTypeReference = ctypeReference;
        SymbolNameReference = symbolNameReference;
        ElementTypeReferences = elementTypeReferences;
    }

    public void ResolveAs(Type type)
    {
        Type = type;
    }

    public Type GetResolvedType()
    {
        if (Type is not null)
            return Type;

        var ctypeName = CTypeReference?.ToString() ?? "??";
        var symbolName = SymbolNameReference?.ToString() ?? "??";
        throw new InvalidOperationException($"The type {ctypeName} / {symbolName} has not been resolved.");
    }

    public override string ToString()
    {
        return $"{nameof(TypeReference)}: {nameof(CTypeReference)}: {CTypeReference}, {nameof(SymbolNameReference)}: {SymbolNameReference}";
    }

    internal bool GetIsResolved()
        => Type is { };
}
