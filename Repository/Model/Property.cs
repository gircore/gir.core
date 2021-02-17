using System.Collections.Generic;
using Repository.Analysis;

namespace Repository.Model
{
    public class Property : Symbol
    {
        public Transfer Transfer { get; }
        public bool Writeable { get; }
        public SymbolReference SymbolReference { get; }
        
        public Property(string nativeName, string managedName, SymbolReference symbolReference, bool writeable, Transfer transfer) : base(nativeName, managedName)
        {
            SymbolReference = symbolReference;
            Writeable = writeable;
            Transfer = transfer;
        }

        public override IEnumerable<SymbolReference> GetSymbolReferences()
        {
            yield return SymbolReference;
        }
    }
}
