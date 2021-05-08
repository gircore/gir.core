using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Repository.Model
{
    internal class SymbolReferenceFactory
    {
        public SymbolReference Create(string? type, string? ctype, NamespaceName currentNamespace)
        {
            return new SymbolReference(
                typeName: GetType(type),
                ctypeName: GetCType(ctype),
                namespaceName: GetNamespace(type, currentNamespace)
            );
        }

        public SymbolReference Create(Xml.AnyType anyType, NamespaceName currentNamespace)
        {
            if (TryCreate(anyType?.Type, currentNamespace, out var type))
                return type;

            if (TryCreate(anyType?.Array?.Type, currentNamespace, out var array))
                return array;

            return Create("void", "none", currentNamespace);
        }

        public SymbolReference Create(Xml.Type type, NamespaceName currentNamespace)
        {
            if (TryCreate(type, currentNamespace, out var symbolReference))
                return symbolReference;

            throw new Exception("Could not create SymbolReference vrom TypeInfo");
        }

        private bool TryCreate(Xml.Type? type, NamespaceName currentNamespace, [MaybeNullWhen(false)] out SymbolReference symbolReference)
        {
            symbolReference = null;

            if (type is null)
                return false;

            symbolReference = Create(type.Name, type.CType, currentNamespace);
            return true;
        }

        public IEnumerable<SymbolReference> Create(IEnumerable<Xml.Implement> implements, NamespaceName currentNamespace)
        {
            var list = new List<SymbolReference>();

            foreach (Xml.Implement implement in implements)
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

        private static TypeName? GetType(string? type)
        {
            if (type is null)
                return null;

            if (!type.Contains("."))
                return new TypeName(type);

            return new TypeName(type.Split('.', 2)[1]);
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
