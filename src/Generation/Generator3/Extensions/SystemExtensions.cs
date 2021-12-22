using System.Collections.Generic;

internal static class SystemExtensions
{
    public static string Join(this IEnumerable<string> ie, string separator)
        => string.Join(separator, ie);
}
