using System;
using System.Runtime.InteropServices;

namespace GLib.Internal;

public partial class CacheHandle
{
    [DllImport(ImportResolver.Library, EntryPoint = "g_async_queue_ref")]
    protected static extern IntPtr Ref(IntPtr queue);

    public partial CacheOwnedHandle OwnedCopy()
    {
        throw new NotSupportedException($"Can't create a copy of a {nameof(CacheHandle)}.");
    }

    public partial CacheUnownedHandle UnownedCopy()
    {
        throw new NotSupportedException($"Can't create a copy of a {nameof(CacheHandle)}.");
    }
}

public partial class CacheOwnedHandle
{
    [DllImport(ImportResolver.Library, EntryPoint = "g_async_queue_unref")]
    private static extern void Unref(IntPtr queue);

    public static partial CacheOwnedHandle FromUnowned(IntPtr ptr)
    {
        throw new NotSupportedException($"Can't create a copy of a {nameof(CacheHandle)}.");
    }

    protected override partial bool ReleaseHandle()
    {
        throw new NotSupportedException($"Can't release a {nameof(CacheHandle)}.");
    }
}
