using System.Collections.Generic;

namespace GLib;

public static class SListExtensions
{
    public static IEnumerable<string?> AsStringsUTF8(this IEnumerable<SListElement> source)
    {
        foreach (var item in source)
        {
            yield return item.ToStringUTF8();
        }
    }
}
