using System;

namespace GObject.Internal;

#if NET6_0

//TODO: Remove this class once support for dotnet 6 is dropped
public static class GTypeProviderHelper
{
    public static Type GetGType<T>() where T : GTypeProvider
    {
        var getGTypeMethod = typeof(T).GetMethod("GetGType");

        if (getGTypeMethod is null)
            throw new Exception($"Method 'GetGType' not found on {typeof(T).Name}");

        return (Type) (getGTypeMethod.Invoke(null, null) ?? throw new Exception($"Method 'GetGType' on {typeof(T).Name} did not return a result"));
    }
}
#endif
