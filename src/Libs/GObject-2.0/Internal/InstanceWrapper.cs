using System;

namespace GObject.Internal;

public static class InstanceWrapper
{
    public static object? WrapNullableHandle<TFallback>(IntPtr handle, bool ownedRef) where TFallback : InstanceFactory, GTypeProvider
    {
        return handle == IntPtr.Zero
            ? null
            : WrapHandle<TFallback>(handle, ownedRef);
    }

    public static object WrapHandle<TFallback>(IntPtr handle, bool ownedRef) where TFallback : InstanceFactory, GTypeProvider
    {
        if (handle == IntPtr.Zero)
            throw new NullReferenceException("Failed to wrap handle: Null handle passed to WrapHandle.");

        if (InstanceCache.TryGetObject(handle, out var obj))
            return obj;

        return DynamicInstanceFactory.Create<TFallback>(handle, ownedRef);
    }
}
