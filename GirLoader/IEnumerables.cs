using System.Collections.Generic;
using System.Linq;
using Gir.Model;

namespace Gir
{
    internal static class IEnumerables
    {
        public static IEnumerable<T> Concat<T>(params IEnumerable<T>[] lists)
        {
            return lists.SelectMany(x => x);
        }

        public static IEnumerable<TypeReference> GetSymbolReferences(this IEnumerable<TypeReferenceProvider> providers)
        {
            return providers.SelectMany(x => x.GetTypeReferences());
        }

        internal static bool AllResolved(this IEnumerable<Resolveable> symbols)
        {
            return symbols.All(x => x.GetIsResolved());
        }

        internal static void Strip<T>(this IEnumerable<T> symbols) where T : Type
        {
            foreach (var symbol in symbols)
                symbol.Strip();
        }
    }
}
