using System.Collections.Generic;

namespace Repository.Model
{
    public interface SymbolReferenceProvider
    {
        IEnumerable<SymbolReference> GetSymbolReferences();
    }
}
