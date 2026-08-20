using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace GirLoader.Output;

internal class TypeReferenceFactory
{
    public TypeReference CreateTypeReference(string? name, string? ctype)
    {
        return new TypeReference(
            symbolNameReference: GetSymbolNameReference(name),
            ctypeReference: GetCType(ctype),
            elementTypeReferences: []
        );
    }

    public AnyTypeReference CreateAnyTypeReference(Input.AnyType anyType)
    {
        if (TryCreateTypeReference(anyType, out var typeReference))
            return typeReference;

        if (TryCreateArrayTypeReference(anyType, out var arrayTypeReference))
            return arrayTypeReference;

        return CreateTypeReference("void", "none");
    }

    private bool TryCreateTypeReference(Input.AnyType anyType, [NotNullWhen(true)] out TypeReference? typeReference)
    {
        if (anyType.Type is null)
        {
            typeReference = null;
            return false;
        }

        typeReference = CreateTypeReferenceFromInputType(anyType.Type);

        return true;
    }

    private bool TryCreateArrayTypeReference(Input.AnyType anyType, [NotNullWhen(true)] out ArrayTypeReference? arrayTypeReference)
    {
        if (anyType.Array is null)
        {
            arrayTypeReference = null;
            return false;
        }

        arrayTypeReference = CreateArrayTypeReferenceFromInputType(anyType.Array);

        return true;
    }

    private TypeReference CreateTypeReferenceFromInputType(Input.Type type)
    {
        return new TypeReference(
            symbolNameReference: GetSymbolNameReference(type.Name),
            ctypeReference: GetCType(type.CType),
            elementTypeReferences: CreateElementTypesFromInputType(type)
        );
    }

    private ArrayTypeReference CreateArrayTypeReferenceFromInputType(Input.ArrayType arrayType)
    {
        var anyTypeReference = CreateAnyTypeReference(arrayType);

        int? length = int.TryParse(arrayType.Length, out var l) ? l : null;
        int? fixedSize = int.TryParse(arrayType.FixedSize, out var f) ? f : null;

        var reference = new ArrayTypeReference(
            anyTypeReference: anyTypeReference,
            symbolNameReference: null,
            ctype: GetCType(arrayType.CType))
        {
            Length = length,
            FixedSize = fixedSize,
            //The fallback is required as gobject-introspection expects an array to be zero terminated,
            //if neither length nor fixedSize are given.
            IsZeroTerminated = arrayType.ZeroTerminated || (length is null && fixedSize is null)
        };

        return arrayType.Name switch
        {
            "GLib.Array" => new GLibArrayTypeReference(reference),
            "GLib.ByteArray" => new GLibByteArrayTypeReference(reference),
            "GLib.PtrArray" => new GLibPtrArrayTypeReference(reference),
            _ => new StandardArrayTypeReference(reference)
        };
    }

    private List<AnyTypeReference> CreateElementTypesFromInputType(Input.Type type)
    {
        var elementTypes = new List<AnyTypeReference>();

        foreach (var elementType in type.ElementTypes)
        {
            if (elementType is Input.Type inputType)
                elementTypes.Add(CreateTypeReferenceFromInputType(inputType));

            if (elementType is Input.ArrayType inputArrayType)
                elementTypes.Add(CreateArrayTypeReferenceFromInputType(inputArrayType));
        }

        return elementTypes;
    }

    public IEnumerable<TypeReference> Create(IEnumerable<Input.Implement> implements)
    {
        var list = new List<TypeReference>();

        foreach (var implement in implements)
        {
            if (implement.Name is null)
                throw new Exception("Implement is missing a name");

            list.Add(CreateTypeReference(implement.Name, null));
        }

        return list;
    }

    public IEnumerable<TypeReference> Create(IEnumerable<Input.Prerequisite> prerequisites)
    {
        var list = new List<TypeReference>();

        foreach (var prerequisite in prerequisites)
        {
            if (prerequisite.Name is null)
                throw new Exception("Prerequisite is missing a name");

            list.Add(CreateTypeReference(prerequisite.Name, null));
        }

        return list;
    }

    private static SymbolNameReference? GetSymbolNameReference(string? name)
    {
        if (name is null)
            return null;

        if (!name.Contains('.'))
            return new SymbolNameReference(name, null);

        var parts = name.Split('.', 2);

        return new SymbolNameReference(
            parts[1],
            new NamespaceName(parts[0])
        );
    }

    private static CTypeReference? GetCType(string? ctype)
    {
        if (ctype is null)
            return null;

        return new CTypeReference(ctype);
    }
}
