using System.Collections.Generic;
using Repository.Analysis;

namespace Repository.Model
{
    public class ReturnValue : ISymbolReferenceProvider
    {
        public Transfer Transfer { get; }
        public bool Nullable { get; }
        public SymbolReference SymbolReference { get; }

        public ReturnValue(SymbolReference symbolReference, Transfer transfer, bool nullable)
        {
            SymbolReference = symbolReference;
            Transfer = transfer;
            Nullable = nullable;
        }

        public IEnumerable<SymbolReference> GetSymbolReferences()
        {
            yield return SymbolReference;
        }
    }
}
