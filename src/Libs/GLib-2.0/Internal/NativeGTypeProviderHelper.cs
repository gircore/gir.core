using System;

namespace GLib.Internal;

#if NET6_0

//TODO: Remove this class once support for dotnet 6 is dropped
public static class NativeGTypeProviderHelper
{
    public static nuint GetGType<T>() where T : NativeGTypeProvider
    {
        var getGTypeMethod = typeof(T).GetMethod(nameof(GetGType));

        if (getGTypeMethod is null)
            throw new Exception($"Method {nameof(GetGType)} not found on {typeof(T).Name}");

        return (nuint) (getGTypeMethod.Invoke(null, null) ?? throw new Exception($"Method {nameof(GetGType)} on {typeof(T).Name} did not return a result"));
    }
}
#endif
