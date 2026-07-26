using System;
using System.Collections.Generic;

namespace GirLoader.Output;

public class ResolveableTypeReference : TypeReference
{
    private Type? _resolvedType;
    public override Type? Type => _resolvedType;

    /// <summary>
    /// Element type references of a container type like GLib.List. GLib.HashTable
    /// has two element type references: the key and the value type.
    /// </summary>
    public IReadOnlyList<TypeReference> ElementTypeReferences { get; }

    public ResolveableTypeReference(SymbolNameReference? symbolNameReference, CTypeReference? ctype, IReadOnlyList<TypeReference>? elementTypeReferences = null)
        : base(symbolNameReference, ctype)
    {
        ElementTypeReferences = elementTypeReferences ?? Array.Empty<TypeReference>();
    }

    public void ResolveAs(Type type)
    {
        _resolvedType = type;
    }
}
