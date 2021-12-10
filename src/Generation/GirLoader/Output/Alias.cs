using System.Collections.Generic;

namespace GirLoader.Output
{
    public class Alias
    {
        public string Name { get; }
        public string CType { get; }
        public Repository Repository { get; }
        public TypeReference TypeReference { get; }

        public Alias(Repository repository, string name, string cType, TypeReference typeReference)
        {
            TypeReference = typeReference;
            Repository = repository;
            Name = name;
            CType = cType;
        }

        public override string ToString()
            => CType;

        internal bool Matches(TypeReference typeReference)
        {
            if (typeReference.CTypeReference?.CType is not null)
                return typeReference.CTypeReference.CType == CType;//Prefer CType

            if (typeReference.SymbolNameReference is not null)
            {
                var nameMatches = typeReference.SymbolNameReference.SymbolName == Name;
                var namespaceMatches = typeReference.SymbolNameReference.NamespaceName == Repository.Namespace.Name;
                var namespaceMissing = typeReference.SymbolNameReference.NamespaceName == null;

                return nameMatches && (namespaceMatches || namespaceMissing);
            }

            return false;
        }
    }
}
