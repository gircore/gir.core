using System.Collections.Generic;
using System.Linq;

namespace Gir.Output.Model
{
    public class Member : Element
    {
        public string Value { get; }

        public Member(ElementName elementName, SymbolName symbolName, string value) : base(elementName, symbolName)
        {
            Value = value;
        }

        public override IEnumerable<TypeReference> GetTypeReferences()
            => Enumerable.Empty<TypeReference>();

        public override bool GetIsResolved()
            => true;
    }
}
