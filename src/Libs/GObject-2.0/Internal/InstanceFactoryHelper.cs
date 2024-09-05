using System;

namespace GObject.Internal;

#if NET6_0

//TODO: Remove this class once support for dotnet 6 is dropped
public static class InstanceFactoryHelper
{
    public static object Create<T>(IntPtr handle, bool ownsHandle) where T : InstanceFactory
    {
        var createMethod = typeof(T).GetMethod(nameof(Create));

        if (createMethod is null)
            throw new Exception($"Method {nameof(Create)} not found on {typeof(T).Name}");

        return createMethod.Invoke(null, new object[]{ handle, ownsHandle }) ?? throw new Exception($"Method {nameof(Create)} on {typeof(T).Name} did not return a result");
    }
}
#endif
