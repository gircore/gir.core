using System;
using System.Collections.Generic;
using System.Linq;
using Generator3.Model;

internal static class Internal
{
    public static string ForEachCall<T>(this IEnumerable<T> list, Func<T, string> func)
        => string.Join(Environment.NewLine, list.Select(func));

    public static string IfNotNullCall<T>(this T? obj, Func<T, string> func)
    {
        return obj is null 
            ? "" 
            : func(obj);
    }

    public static string? IfNotNullAppendNewline(this string? str)
    {
        if (string.IsNullOrEmpty(str))
            return string.Empty;

        return str + Environment.NewLine;
    }

    public static string Join(this IEnumerable<string> ie, string separator)
        => string.Join(separator, ie);

    public static string GetDirection(this GirModel.Parameter parameter, ParameterDirection @in, ParameterDirection @out, ParameterDirection outCallerAllocates, ParameterDirection inout)
    {
        return parameter switch {
            { Direction: GirModel.Direction.InOut } => inout.Value,
            { Direction: GirModel.Direction.Out, CallerAllocates: true } => outCallerAllocates.Value,
            { Direction: GirModel.Direction.Out } => @out.Value,
            _ => @in.Value
        };
    }
}
