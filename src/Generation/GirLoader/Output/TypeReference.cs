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

    /// <summary>
    /// Element type references of this type reference: container types like
    /// GLib.List carry their element type here, GLib.HashTable carries two
    /// (the key and the value type) and arrays carry exactly one. Empty if
    /// there are no element types.
    /// </summary>
    public IReadOnlyList<TypeReference> ElementTypeReferences { get; }

    #endregion

    protected TypeReference(SymbolNameReference? symbolNameReference, CTypeReference? ctypeReference, IReadOnlyList<TypeReference>? elementTypeReferences = null)
    {
        CTypeReference = ctypeReference;
        SymbolNameReference = symbolNameReference;
        ElementTypeReferences = elementTypeReferences ?? Array.Empty<TypeReference>();
    }

    public Type GetResolvedType()
    {
        if (Type is not null)
            return Type;

        var ctypeName = CTypeReference?.ToString() ?? "??";
        var symbolName = SymbolNameReference?.ToString() ?? "??";
        throw new InvalidOperationException($"The type {ctypeName} / {symbolName} has not been resolved.");
    }

    internal abstract GirModel.AnyType GetResolvedAnyType();

    internal IReadOnlyList<GirModel.AnyType> GetResolvedElementTypes()
        => ElementTypeReferences
            .Select(x => x.GetResolvedAnyType())
            .ToArray();

    public override string ToString()
    {
        return $"{nameof(TypeReference)}: {nameof(CTypeReference)}: {CTypeReference}, {nameof(SymbolNameReference)}: {SymbolNameReference}";
    }
}
