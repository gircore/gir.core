using System.Collections.Generic;
using Repository.Analysis;

namespace Repository.Model
{
    public class Property : Symbol
    {
        public Transfer Transfer { get; }
        public bool Writeable { get; }
        public SymbolReference SymbolReference { get; }
        
        public Property(string name, string managedName, SymbolReference symbolReference, bool writeable, Transfer transfer) : base(name, name, managedName, managedName)
        {
            SymbolReference = symbolReference;
            Writeable = writeable;
            Transfer = transfer;
        }

        public override IEnumerable<SymbolReference> GetSymbolReferences()
        {
            yield return SymbolReference;
        }

        internal override bool GetIsResolved()
            => SymbolReference.IsResolved;
    }
}
