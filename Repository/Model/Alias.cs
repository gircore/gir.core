using System.Collections.Generic;

namespace Repository.Model
{
    public class Alias : Element
    {
        public Namespace Namespace { get; }
        public TypeReference TypeReference { get; }

        public Alias(Namespace @namespace, ElementName elementName, SymbolName symbolName, TypeReference typeReference) : base(elementName, symbolName)
        {
            TypeReference = typeReference;
            Namespace = @namespace;
        }

        public override IEnumerable<TypeReference> GetTypeReferences()
        {
            yield return TypeReference;
        }

        public override bool GetIsResolved()
            => TypeReference.GetIsResolved();
    }
}
