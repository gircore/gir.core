using System.Linq;

namespace Generator
{
    public static class ObjectExtension
    {
        #region Methods

        public static bool In<T>(this T obj, params T[] p)
        {
            return p.Contains(obj);
        }

        #endregion
    }
}
