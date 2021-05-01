using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Repository.Analysis;
using Repository.Xml;

namespace Repository.Services
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

        public SymbolReference Create(Typed typed, NamespaceName currentNamespace)
        {
            if (TryCreate(typed?.Type, currentNamespace, out var type))
                return type;

            if (TryCreate(typed?.Array?.Type, currentNamespace, out var array))
                return array;

            return Create("void", "none", currentNamespace);
        }

        public SymbolReference Create(TypeInfo typeInfo, NamespaceName currentNamespace)
        {
            if (TryCreate(typeInfo, currentNamespace, out var symbolReference))
                return symbolReference;

            throw new Exception("Could not create SymbolReference vrom TypeInfo");
        }

        private bool TryCreate(TypeInfo? typeInfo, NamespaceName currentNamespace, [MaybeNullWhen(false)] out SymbolReference symbolReference)
        {
            symbolReference = null;

            if (typeInfo is null)
                return false;

            symbolReference = Create(typeInfo.Name, typeInfo.CType, currentNamespace);
            return true;
        }

        public IEnumerable<SymbolReference> Create(IEnumerable<ImplementInfo> implements, NamespaceName currentNamespace)
        {
            var list = new List<SymbolReference>();

            foreach (ImplementInfo implement in implements)
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
