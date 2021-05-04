using System.Collections.Generic;
using System.Linq;

namespace Repository.Model
{
    public class Member : Element
    {
        public string Value { get; }

        public Member(ElementName elementName, SymbolName symbolName, string value) : base(elementName, symbolName)
        {
            Value = value;
        }

        public override IEnumerable<SymbolReference> GetSymbolReferences()
            => Enumerable.Empty<SymbolReference>();

        public override bool GetIsResolved()
            => true;
    }
}
