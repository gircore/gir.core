using System.Collections.Generic;

namespace Repository.Model
{
    public class Constant : Element
    {
        public string Value { get; }
        public SymbolReference SymbolReference { get; }

        public Constant(ElementName elementName, SymbolName symbolName, SymbolReference symbolReference, string value) : base(elementName, symbolName)
        {
            SymbolReference = symbolReference;
            Value = value;
        }

        public override IEnumerable<SymbolReference> GetSymbolReferences()
        {
            yield return SymbolReference;
        }

        public override bool GetIsResolved()
            => SymbolReference.GetIsResolved();
    }
}
