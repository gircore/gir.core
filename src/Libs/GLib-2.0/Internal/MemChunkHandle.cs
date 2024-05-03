using System;

namespace GLib.Internal;

public partial class MemChunkHandle
{
    public partial MemChunkOwnedHandle OwnedCopy()
    {
        throw new NotSupportedException($"Can't create a copy of a {nameof(MemChunkHandle)}.");
    }

    public partial MemChunkUnownedHandle UnownedCopy()
    {
        throw new NotSupportedException($"Can't create a copy of a {nameof(MemChunkHandle)}.");
    }
}

public partial class MemChunkOwnedHandle
{
    public static partial MemChunkOwnedHandle FromUnowned(IntPtr ptr)
    {
        throw new NotSupportedException($"Can't create a copy of a {nameof(MemChunkOwnedHandle)}.");
    }

    protected override partial bool ReleaseHandle()
    {
        throw new NotSupportedException($"Can't free a {nameof(MemChunkOwnedHandle)}.");
    }
}
