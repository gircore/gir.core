/*
 * This class is shared among different projects to provide an
 * convenience functions
 *
 * As this class has no namespace it can be used anywhere it is
 * linked. The code is kept clean as it does not require an
 * object instance. 
 */

using System;
using System.Linq;

internal static class Element
{
    public static bool IsOneOf<T>(this T value, params T[] list)
    {
        return list.Contains(value);
    }

    public static K Map<T, K>(this T value, Func<T, K> f) => f(value);
}
