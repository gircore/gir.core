using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace GirLoader.Output.Model
{
    internal class TypeReferenceFactory
    {
        public ResolveableTypeReference CreateResolveable(string? name, string? ctype, NamespaceName currentNamespace)
        {
            return new ResolveableTypeReference(
                originalName: GetName(name),
                ctype: GetCType(ctype),
                namespaceName: GetNamespace(name, currentNamespace)
            );
        }

        public TypeReference Create(Input.Model.AnyType anyType, NamespaceName currentNamespace)
        {
            if (TryCreateResolveableTypeReference(anyType, currentNamespace, out var typeRefernece))
                return typeRefernece;

            if (TryCreateArrayTypeReference(anyType, currentNamespace, out var arrayTypeRefernece))
                return arrayTypeRefernece;

            return CreateResolveable("void", "none", currentNamespace);
        }

        private bool TryCreateResolveableTypeReference(Input.Model.AnyType anyType, NamespaceName currentNamespace, [NotNullWhen(true)] out TypeReference? typeReference)
        {
            if (anyType.Type is null)
            {
                typeReference = null;
                return false;
            }

            typeReference = new ResolveableTypeReference(
                originalName: GetName(anyType.Type.Name),
                ctype: GetCType(anyType.Type.CType),
                namespaceName: GetNamespace(anyType.Type.Name, currentNamespace));

            return true;
        }

        private bool TryCreateArrayTypeReference(Input.Model.AnyType anyType, NamespaceName currentNamespace, [NotNullWhen(true)] out ArrayTypeReference? arrayTypeReference)
        {
            if (anyType.Array is null)
            {
                arrayTypeReference = null;
                return false;
            }

            var typeReference = Create(anyType.Array, currentNamespace);

            int? length = int.TryParse(anyType.Array.Length, out var l) ? l : null;
            int? fixedSize = int.TryParse(anyType.Array.FixedSize, out var f) ? f : null;

            arrayTypeReference = new ArrayTypeReference(
                typeReference: typeReference,
                originalName: null,
                ctype: GetCType(anyType.Array.CType),
                namespaceName: GetNamespace(anyType.Array.Type?.Name, currentNamespace))
            {
                Length = length, 
                FixedSize = fixedSize, 
                IsZeroTerminated = anyType.Array.ZeroTerminated
            };
            
            return true;
        }

        public IEnumerable<TypeReference> Create(IEnumerable<Input.Model.Implement> implements, NamespaceName currentNamespace)
        {
            var list = new List<TypeReference>();

            foreach (Input.Model.Implement implement in implements)
            {
                if (implement.Name is null)
                    throw new Exception("Implement is missing a name");

                list.Add(CreateResolveable(implement.Name, null, currentNamespace));
            }

            return list;
        }

        private static NamespaceName GetNamespace(string? type, NamespaceName currentNamespace)
        {
            if (type is null)
                return currentNamespace;

            if (!type.Contains("."))
                return currentNamespace;

            return new NamespaceName(type.Split('.', 2)[0]);
        }

        private static SymbolName? GetName(string? name)
        {
            if (name is null)
                return null;

            if (!name.Contains("."))
                return new SymbolName(name);

            return new SymbolName(name.Split('.', 2)[1]);
        }

        private static CType? GetCType(string? ctype)
        {
            if (ctype is null)
                return null;

            return new CType(ctype);
        }
    }
}
