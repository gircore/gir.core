using System.Collections.Generic;
using Repository.Analysis;

namespace Repository.Model
{
    public interface  SymbolReferenceProvider
    {
        IEnumerable<SymbolReference> GetSymbolReferences();
    }
}
