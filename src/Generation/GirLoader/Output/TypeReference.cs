using System;
using System.Collections.Generic;
using System.Linq;

namespace GirLoader.Output;

public abstract class TypeReference
{
    #region Properties
    public CTypeReference? CTypeReference { get; }
    public SymbolNameReference? SymbolNameReference { get; }
    public abstract Type? Type { get; }

    #endregion

    protected TypeReference(SymbolNameReference? symbolNameReference, CTypeReference? ctypeReference)
    {
        CTypeReference = ctypeReference;
        SymbolNameReference = symbolNameReference;
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

    internal IReadOnlyList<GirModel.AnyType> GetResolvedElementTypes() => this switch
    {
        ResolveableTypeReference resolveableTypeReference => resolveableTypeReference.ElementTypeReferences
            .Where(x => x.GetIsResolved())
            .Select(x => GirModel.AnyType.From(x.GetResolvedType()))
            .ToArray(),
        _ => Array.Empty<GirModel.AnyType>()
    };
}
