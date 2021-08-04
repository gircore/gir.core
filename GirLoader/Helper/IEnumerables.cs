using System.Collections.Generic;
using System.Linq;
using GirLoader.Output.Model;

namespace GirLoader.Helper
{
    internal static class IEnumerables
    {
        public static IEnumerable<T> Concat<T>(params IEnumerable<T>[] lists)
        {
            return lists.SelectMany(x => x);
        }

        internal static void Strip<T>(this IEnumerable<T> symbols) where T : Type
        {
            foreach (var symbol in symbols)
                symbol.Strip();
        }
    }
}
