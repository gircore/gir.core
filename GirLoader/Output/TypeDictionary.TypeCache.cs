using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Gir.Output
{
    internal partial class TypeDictionary
    {
        private class TypeCache
        {
            public Model.Repository? Repository { get; }
            private readonly HashSet<Model.Type> _types = new();

            public TypeCache(Model.Repository? repository)
            {
                Repository = repository;
            }

            public void Add(Model.Type type)
            {
                _types.Add(type);
            }

            public bool TryLookup(Model.TypeReference typeReference, [MaybeNullWhen(false)] out Model.Type type)
            {
                type = _types.FirstOrDefault(x => CheckTypeReference(x, typeReference));

                return type is not null;
            }

            private bool CheckTypeReference(Model.Type type, Model.TypeReference typeReference)
            {
                if (!CheckNamespace(type, typeReference))
                    return false;

                return type.TypeName == typeReference.TypeName;
            }

            private static bool CheckNamespace(Model.Type type, Model.TypeReference typeReference) => (symbol: type, symbolReference: typeReference) switch
            {
                ({ Repository: { } }, { NamespaceName: null }) => false,
                ({ Repository: { Namespace: {Name: { } n1 }} }, { NamespaceName: { } n2 }) when n1 != n2 => false,
                _ => true
            };
        }
    }
}
