using System.Linq;

namespace Gir
{
    public static class ObjectExtension
    {
        public static bool In<T>(this T obj, params T[] p)
        {
            return p.Contains(obj);
        }
    }
}