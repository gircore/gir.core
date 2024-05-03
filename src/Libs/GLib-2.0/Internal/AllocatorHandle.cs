using System;
using System.Runtime.InteropServices;

namespace GLib.Internal;

public partial class AllocatorHandle
{
    public partial AllocatorOwnedHandle OwnedCopy()
    {
        throw new NotSupportedException($"Can't create a copy of a {nameof(AllocatorHandle)}.");
    }

    public partial AllocatorUnownedHandle UnownedCopy()
    {
        throw new NotSupportedException($"Can't create a copy of a {nameof(AllocatorHandle)}.");
    }
}

public partial class AllocatorOwnedHandle
{
    [DllImport(ImportResolver.Library, EntryPoint = "g_allocator_free")]
    private static extern void Free(IntPtr queue);

    public static partial AllocatorOwnedHandle FromUnowned(IntPtr ptr)
    {
        throw new NotSupportedException($"Can't create a copy of a {nameof(AllocatorHandle)}.");
    }

    protected override partial bool ReleaseHandle()
    {
        Free(handle);
        return true;
    }
}
