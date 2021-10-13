using System;
using System.Collections.Generic;
using System.Linq;

public static class Extensions
{
    public static string Write<T>(this IEnumerable<T> list, Func<T, string> func)
        => string.Join(Environment.NewLine, list.Select(func));
}
