using System.Linq;

namespace Generator
{
    internal static class ObjectExtension
    {
        public static bool In<T>(this T obj, params T[] parameters)
            => parameters.Contains(obj);
    }
}
