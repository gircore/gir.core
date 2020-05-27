using System.Linq;

namespace Generator
{
    public static class ObjectExtension
    {
        public static bool In<T>(this T obj, params T[] p)
        {
            return p.Contains(obj);
        }
    }
}