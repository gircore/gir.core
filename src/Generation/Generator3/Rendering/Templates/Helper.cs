using System;
using System.Collections.Generic;
using System.Linq;

namespace Generator3.Rendering.Templates
{
    public static class Helper
    {
        public static string Write<T>(this IEnumerable<T> list, Func<T, string> func)
            => string.Join(Environment.NewLine, list.Select(func));
    }
}
