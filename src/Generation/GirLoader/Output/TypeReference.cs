using System;

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
}
