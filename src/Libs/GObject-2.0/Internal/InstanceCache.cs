using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

namespace GObject.Internal;

internal static class InstanceCache
{
    private static readonly object Lock = new();
    private static readonly Dictionary<IntPtr, ToggleRef> Cache = new();
    internal static int ObjectCount
    {
        get
        {
            lock (Lock)
            {
                return Cache.Count;
            }
        }
    }

    public static bool TryGetObject(IntPtr handle, [NotNullWhen(true)] out GObject.Object? obj)
    {
        lock (Lock)
        {
            if (Cache.TryGetValue(handle, out ToggleRef? toggleRef))
            {
                if (toggleRef.Object is not null)
                {
                    obj = toggleRef.Object;
                    return true;
                }
            }
        }

        obj = null;
        return false;
    }

    public static unsafe void Add(IntPtr handle, GObject.Object obj)
    {
        lock (Cache)
        {
            Cache[handle] = new ToggleRef(obj);
            ToggleRegistration.AddToggleRef(handle, &ToggleNotify);
            Object.Unref(handle);
        }

        Debug.WriteLine($"Handle {handle}: Added object of type '{obj.GetType()}' to {nameof(InstanceCache)}");
    }

    public static unsafe void Remove(IntPtr handle)
    {
        lock (Cache)
        {
            if (Cache.Remove(handle))
                ToggleRegistration.RemoveToggleRef(handle, &ToggleNotify);
        }

        Debug.WriteLine($"Handle {handle}: Removed object from {nameof(InstanceCache)}.");
    }

    [UnmanagedCallersOnly]
    private static void ToggleNotify(IntPtr data, IntPtr @object, int isLastRef)
    {
        lock (Lock)
        {
            if (Cache.TryGetValue(@object, out var toggleRef))
                toggleRef.ToggleReference(isLastRef != 0);
            else
                Debug.WriteLine($"Handle {@object}: Could not toggle to {isLastRef} as there is no toggle reference.");
        }
    }
}
