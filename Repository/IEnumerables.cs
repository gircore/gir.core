using System.Collections.Generic;
using System.Linq;
using Repository.Analysis;
using Repository.Model;

namespace Repository
{
    internal static class IEnumerables
    {
        public static IEnumerable<T> Concat<T>(params IEnumerable<T>[] lists)
        {
            return lists.SelectMany(x => x);
        }
        
        public static IEnumerable<SymbolReference> GetSymbolReferences(this IEnumerable<ISymbolReferenceProvider> providers)
        {
            return providers.SelectMany(x => x.GetSymbolReferences());
        }
    }
}
