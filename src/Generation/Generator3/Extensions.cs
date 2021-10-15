using System;
using System.Collections.Generic;
using System.Linq;

public static class Extensions
{
    public static string ForEachCall<T>(this IEnumerable<T> list, Func<T, string> func)
        => string.Join(Environment.NewLine, list.Select(func));

    public static string IfNotNullCall<T>(this T? obj, Func<T, string> func)
    {
        return obj is null 
            ? "" 
            : func(obj);
    }
}
