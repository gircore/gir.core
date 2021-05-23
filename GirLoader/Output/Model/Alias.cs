using System.Collections.Generic;

namespace GirLoader.Output.Model
{
    public class Alias : TypeReferenceProvider, Resolveable
    {
        public SymbolName Name { get; set; }
        public CTypeName CTypeName { get; }
        public Repository Repository { get; }
        public TypeReference TypeReference { get; }

        public Alias(Repository repository, SymbolName name, CTypeName cTypeName, TypeReference typeReference)
        {
            TypeReference = typeReference;
            Repository = repository;
            Name = name;
            CTypeName = cTypeName;
        }

        public IEnumerable<TypeReference> GetTypeReferences()
        {
            yield return TypeReference;
        }

        public bool GetIsResolved()
            => TypeReference.GetIsResolved();
    }
}
