using System;

namespace GObject.Internal;

public static class InstanceWrapper
{
    public static object? WrapNullableHandle<TFallback>(IntPtr handle, bool ownedRef) where TFallback : GObject.Object, InstanceFactory, GTypeProvider
    {
        return handle == IntPtr.Zero
            ? null
            : WrapHandle<TFallback>(handle, ownedRef);
    }

    public static object WrapHandle<TFallback>(IntPtr handle, bool ownedRef) where TFallback : GObject.Object, InstanceFactory, GTypeProvider
    {
        if (handle == IntPtr.Zero)
            throw new NullReferenceException("Failed to wrap handle: Null handle passed to WrapHandle.");

        var result = InstanceCache.TryGetObject(handle, out var obj)
            ? obj
            : DynamicInstanceFactory.Create<TFallback>(handle, ownedRef); //owned ref information for records only

        if (result is GObject.Object && ownedRef)
        {
            //For classes only:
            //If a system boundary is crossed with transfer full to C# we remove
            //a ref because there is always a ref for dotnet in addition to C.
            //If we would not do this, we would leak memory because there would be
            //2 refs and only dotnet would unref.
            Object.TakeRef(handle);
            Object.Unref(handle);
        }

        return result;
    }
}
