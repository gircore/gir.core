using System;

namespace GObject.Internal;

#if NET6_0

//TODO: Remove this class once support for dotnet 6 is dropped
public static class GTypeProviderHelper
{
    public static nuint GetGType<T>() where T : GObject.Object, GTypeProvider
    {
        var getGTypeMethod = typeof(T).GetMethod(nameof(GObject.Object.GetGType));

        if (getGTypeMethod is null)
            throw new Exception($"Method {nameof(GObject.Object.GetGType)} not found on {typeof(T).Name}");

        return (nuint) (getGTypeMethod.Invoke(null, null) ?? throw new Exception($"Method {nameof(GObject.Object.GetGType)} on {typeof(T).Name} did not return a result"));
    }
}
#endif
