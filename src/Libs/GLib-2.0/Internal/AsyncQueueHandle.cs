using System;
using System.Runtime.InteropServices;

namespace GLib.Internal;

public partial class AsyncQueueHandle
{
    [DllImport(ImportResolver.Library, EntryPoint = "g_async_queue_ref")]
    protected static extern IntPtr Ref(IntPtr queue);

    public partial AsyncQueueOwnedHandle OwnedCopy()
    {
        return new AsyncQueueOwnedHandle(Ref(handle));
    }

    public partial AsyncQueueUnownedHandle UnownedCopy()
    {
        return new AsyncQueueUnownedHandle(Ref(handle));
    }
}

public partial class AsyncQueueOwnedHandle
{
    [DllImport(ImportResolver.Library, EntryPoint = "g_async_queue_unref")]
    private static extern void Unref(IntPtr queue);

    public static partial AsyncQueueOwnedHandle FromUnowned(IntPtr ptr)
    {
        return new AsyncQueueOwnedHandle(Ref(ptr));
    }

    protected override partial bool ReleaseHandle()
    {
        Unref(handle);
        return true;
    }
}
