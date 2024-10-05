using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using GLib;

namespace GObject.Internal;

public static partial class ObjectMapper
{
    private static readonly object Lock = new();
    private static readonly Dictionary<IntPtr, ToggleRef> WrapperObjects = new();

    public static int ObjectCount
    {
        get
        {
            lock (Lock)
            {
                return WrapperObjects.Count;
            }
        }
    }

    public static bool TryGetObject<T>(IntPtr handle, [NotNullWhen(true)] out T? obj) where T : class, IHandle
    {
        lock (Lock)
        {
            if (WrapperObjects.TryGetValue(handle, out ToggleRef? weakRef))
            {
                if (weakRef.Object is not null)
                {
                    obj = (T) weakRef.Object;
                    return true;
                }
            }
        }

        obj = null;
        return false;
    }

    public static void Map(IntPtr handle, object obj, bool ownedRef)
    {
        lock (Lock)
        {
            WrapperObjects[handle] = new ToggleRef(handle, obj, ownedRef);
            ToggleRegistration.AddToggleRef(handle);
        }

        Debug.WriteLine($"Handle {handle}: Mapped object of type '{obj.GetType()}' as owned ref '{ownedRef}'.");
    }

    public static void Unmap(IntPtr handle)
    {
        lock (Lock)
        {
            if (WrapperObjects.Remove(handle))
                ToggleRegistration.RemoveToggleRef(handle);
        }

        Debug.WriteLine($"Handle {handle}: Unmapped object.");
    }

    [UnmanagedCallersOnly]
    private static void ToggleNotify(IntPtr data, IntPtr @object, int isLastRef)
    {
        lock (Lock)
        {
            if (WrapperObjects.TryGetValue(@object, out var toggleRef))
                toggleRef.ToggleReference(isLastRef != 0);
            else
                Debug.WriteLine($"Handle {@object}: Could not toggle to {isLastRef} as there is no toggle reference.");
        }
    }
}
