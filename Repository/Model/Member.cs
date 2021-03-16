using System.Collections.Generic;
using System.Linq;
using Repository.Analysis;

namespace Repository.Model
{
    public class Member : Element
    {
        public string Value { get; }

        public Member(ElementName elementName, ElementManagedName elementManagedName, string value) : base(elementName, elementManagedName)
        {
            Value = value;
        }

        public override IEnumerable<SymbolReference> GetSymbolReferences()
            => Enumerable.Empty<SymbolReference>();

        public override bool GetIsResolved()
            => true;
    }
}
