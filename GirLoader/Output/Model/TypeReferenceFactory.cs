using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace GirLoader.Output.Model
{
    internal class TypeReferenceFactory
    {
        public TypeReference Create(string? name, string? ctype, NamespaceName currentNamespace)
        {
            return new TypeReference(
                originalName: GetName(name),
                ctypeName: GetCType(ctype),
                namespaceName: GetNamespace(name, currentNamespace)
            );
        }

        public TypeReference Create(Input.Model.AnyType anyType, NamespaceName currentNamespace)
        {
            if (TryCreate(anyType?.Type, currentNamespace, out var type))
                return type;

            if (TryCreate(anyType?.Array?.Type, currentNamespace, out var array))
                return array;

            return Create("void", "none", currentNamespace);
        }

        public TypeReference Create(Input.Model.Type type, NamespaceName currentNamespace)
        {
            if (TryCreate(type, currentNamespace, out var symbolReference))
                return symbolReference;

            throw new Exception("Could not create SymbolReference vrom TypeInfo");
        }

        private bool TryCreate(Input.Model.Type? type, NamespaceName currentNamespace, [MaybeNullWhen(false)] out TypeReference typeReference)
        {
            typeReference = null;

            if (type is null)
                return false;

            typeReference = Create(type.Name, type.CType, currentNamespace);
            return true;
        }

        public IEnumerable<TypeReference> Create(IEnumerable<Input.Model.Implement> implements, NamespaceName currentNamespace)
        {
            var list = new List<TypeReference>();

            foreach (Input.Model.Implement implement in implements)
            {
                if (implement.Name is null)
                    throw new Exception("Implement is missing a name");

                list.Add(Create(implement.Name, null, currentNamespace));
            }

            return list;
        }

        private static NamespaceName? GetNamespace(string? type, NamespaceName currentNamespace)
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

        private static CTypeName? GetCType(string? ctype)
        {
            if (ctype is null)
                return null;

            ctype = ctype
                .Replace("*", "")
                .Replace("const ", "")
                .Replace("volatile ", "")
                .Replace(" const", "");

            return new CTypeName(ctype);
        }
    }
}
