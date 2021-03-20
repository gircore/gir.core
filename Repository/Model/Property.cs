using System.Collections.Generic;
using Repository.Analysis;

namespace Repository.Model
{
    public class Property : Element
    {
        public Transfer Transfer { get; }
        public bool Writeable { get; }
        public SymbolReference SymbolReference { get; }
        
        public Property(ElementName elementName, SymbolName symbolName, SymbolReference symbolReference, bool writeable, Transfer transfer) : base(elementName, symbolName)
        {
            SymbolReference = symbolReference;
            Writeable = writeable;
            Transfer = transfer;
        }

        public override IEnumerable<SymbolReference> GetSymbolReferences()
        {
            yield return SymbolReference;
        }

        public override bool GetIsResolved()
            => SymbolReference.GetIsResolved();
    }
}
