using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

namespace GObject.Internal;

internal static class InstanceCache
{
    internal static readonly object Lock = new();
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
                    obj = toggleRef.Object.Instance ?? throw new Exception($"Handle {handle} misses a managed counterpart.");
                    return true;
                }
            }
        }

        obj = null;
        return false;
    }

    public static unsafe void Add(ObjectHandle handle)
    {
        var ptr = handle.DangerousGetHandle();

        lock (Lock)
        {
            // Create cache entry immediately so ToggleNotify can find it even if native add is delayed
            Cache[ptr] = new ToggleRef(handle);
        }

        // Schedule native registration and Unref on the GLib main context (async)
        var addSource = new GLib.Internal.SourceFuncAsyncHandler(() =>
        {
            ToggleRegistration.AddToggleRef(ptr, &ToggleNotify);
            Object.Unref(ptr);

            return false; // one-shot
        });

        GLib.Internal.MainContext.Invoke(GLib.Internal.MainContextUnownedHandle.NullHandle, addSource.NativeCallback, IntPtr.Zero);

        Console.WriteLine($"Handle {handle}: Added object of type '{handle.Instance?.GetType()}' to {nameof(InstanceCache)}");
    }

    public static unsafe void Remove(IntPtr handle)
    {
        // Schedule native removal on the main context. After the native removal is executed
        // remove the cache entry under the InstanceCache lock to preserve ordering.
        var removeSource = new GLib.Internal.SourceFuncAsyncHandler(() =>
        {
            ToggleRegistration.RemoveToggleRef(handle, &ToggleNotify);

            lock (Lock)
            {
                Cache.Remove(handle);
            }

            return false;
        });

        GLib.Internal.MainContext.Invoke(GLib.Internal.MainContextUnownedHandle.NullHandle, removeSource.NativeCallback, IntPtr.Zero);

        Console.WriteLine($"Handle {handle}: Removed object from {nameof(InstanceCache)}.");
    }

    [UnmanagedCallersOnly]
    private static void ToggleNotify(IntPtr data, IntPtr @object, int isLastRef)
    {
        lock (Lock)
        {
            Console.WriteLine($"Toggle reference from handle {@object}, is last ref: {isLastRef}.");
            if (Cache.TryGetValue(@object, out var toggleRef))
                toggleRef.ToggleReference(isLastRef != 0);
            else
                Console.WriteLine($"Handle {@object}: Could not toggle to {isLastRef} as there is no toggle reference.");
        }
    }

    [DllImport(ImportResolver.Library, EntryPoint = "g_type_name_from_instance")]
    private static extern GLib.Internal.NonNullableUtf8StringUnownedHandle TypeNameFromInstance(IntPtr instance);
}
