using System.Collections.Generic;
using Repository.Analysis;

namespace Repository.Model
{
    public class ReturnValue : ISymbolReferenceProvider
    {
        public Transfer Transfer { get; }
        public bool Nullable { get; }
        public SymbolReference SymbolReference { get; }
        public Array? Array { get; }

        public ReturnValue(SymbolReference symbolReference, Transfer transfer, bool nullable, Array? array = null)
        {
            SymbolReference = symbolReference;
            Transfer = transfer;
            Nullable = nullable;
            Array = array;
        }

        public IEnumerable<SymbolReference> GetSymbolReferences()
        {
            yield return SymbolReference;
        }
    }
}
