using System.Collections.Generic;
using Repository.Analysis;

namespace Repository.Model
{
    public class Constant : Symbol
    {
        public string Value { get; }
        public ISymbolReference SymbolReference { get; }
        
        public Constant(string nativeName, string managedName, ISymbolReference symbolReference, string value) : base(nativeName, managedName)
        {
            SymbolReference = symbolReference;
            Value = value;
        }

        public override IEnumerable<ISymbolReference> GetSymbolReferences()
        {
            yield return SymbolReference;
        }
    }
}
