using System.Collections.Generic;
using Repository.Analysis;

namespace Repository.Model
{
    public class Constant : Symbol
    {
        public string Value { get; }
        public SymbolReference SymbolReference { get; }
        
        public Constant(string nativeName, string managedName, SymbolReference symbolReference, string value) : base(nativeName, managedName)
        {
            SymbolReference = symbolReference;
            Value = value;
        }

        public override IEnumerable<SymbolReference> GetSymbolReferences()
        {
            yield return SymbolReference;
        }
    }
}
