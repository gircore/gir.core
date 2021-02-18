using System.Collections.Generic;
using Repository.Analysis;

namespace Repository.Model
{
    public class Field : Symbol
    {
        public SymbolReference SymbolReference { get; }
        
        public Field(string nativeName, string managedName, SymbolReference symbolReference) : base(nativeName, managedName)
        {
            SymbolReference = symbolReference;
        }

        public override IEnumerable<SymbolReference> GetSymbolReferences()
        {
            yield return SymbolReference;
        }
    }
}
