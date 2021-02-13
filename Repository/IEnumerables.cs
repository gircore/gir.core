using System.Collections.Generic;
using System.Linq;

namespace Repository
{
    public static class IEnumerables
    {
        public static IEnumerable<T> Concat<T>(params IEnumerable<T>[] lists)
        {
            return lists.SelectMany(x => x);
        }

    }
}
