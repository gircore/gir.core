using System.Collections.Generic;
using Repository.Analysis;

namespace Repository.Model
{
    public class ReturnValue : ISymbolReferenceProvider
    {
        public Transfer Transfer { get; }
        public bool Nullable { get; }
        public ISymbolReference SymbolReference { get; }

        public ReturnValue(ISymbolReference symbolReference, Transfer transfer, bool nullable)
        {
            SymbolReference = symbolReference;
            Transfer = transfer;
            Nullable = nullable;
        }

        public IEnumerable<ISymbolReference> GetSymbolReferences()
        {
            yield return SymbolReference;
        }
    }
}
