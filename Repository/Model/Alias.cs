using System.Collections.Generic;

namespace Repository.Model
{
    public class Alias : Element
    {
        public Repository Repository { get; }
        public TypeReference TypeReference { get; }

        public Alias(Repository repository, ElementName elementName, SymbolName symbolName, TypeReference typeReference) : base(elementName, symbolName)
        {
            TypeReference = typeReference;
            Repository = repository;
        }

        public override IEnumerable<TypeReference> GetTypeReferences()
        {
            yield return TypeReference;
        }

        public override bool GetIsResolved()
            => TypeReference.GetIsResolved();
    }
}
