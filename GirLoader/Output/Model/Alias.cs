using System.Collections.Generic;

namespace GirLoader.Output.Model
{
    public class Alias : TypeReferenceProvider, Resolveable
    {
        public SymbolName Name { get; set; }
        public CType CType { get; }
        public Repository Repository { get; }
        public ResolveableTypeReference TypeReference { get; }

        public Alias(Repository repository, SymbolName name, CType cType, ResolveableTypeReference typeReference)
        {
            TypeReference = typeReference;
            Repository = repository;
            Name = name;
            CType = cType;
        }

        public IEnumerable<TypeReference> GetTypeReferences()
        {
            yield return TypeReference;
        }

        public bool GetIsResolved()
            => TypeReference.GetIsResolved();

        public override string ToString()
        {
            return CType.ToString();
        }
    }
}
