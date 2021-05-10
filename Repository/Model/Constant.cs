using System.Collections.Generic;

namespace Repository.Model
{
    public class Constant : Element
    {
        public string Value { get; }
        public TypeReference TypeReference { get; }

        public Constant(ElementName elementName, SymbolName symbolName, TypeReference typeReference, string value) : base(elementName, symbolName)
        {
            TypeReference = typeReference;
            Value = value;
        }

        public override IEnumerable<TypeReference> GetTypeReferences()
        {
            yield return TypeReference;
        }

        public override bool GetIsResolved()
            => TypeReference.GetIsResolved();
    }
}
