using System.Collections.Generic;
using Repository.Analysis;

namespace Repository.Model
{
    public class Alias : Element
    {
        public Namespace Namespace { get; }
        public SymbolReference SymbolReference { get; }

        public Alias(Namespace @namespace, ElementName elementName, SymbolName symbolName, SymbolReference symbolReference) : base(elementName, symbolName)
        {
            SymbolReference = symbolReference;
            Namespace = @namespace;
        }

        public override IEnumerable<SymbolReference> GetSymbolReferences()
        {
            yield return SymbolReference;
        }

        public override bool GetIsResolved()
            => SymbolReference.GetIsResolved();
    }
}
