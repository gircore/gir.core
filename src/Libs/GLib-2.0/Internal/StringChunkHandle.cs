using System;
using System.Runtime.InteropServices;

namespace GLib.Internal;


public abstract partial class StringChunkHandle
{
    public partial StringChunkOwnedHandle OwnedCopy()
    {
        throw new NotSupportedException("Can't create a copy of a string chunk handle");
    }

    public partial StringChunkUnownedHandle UnownedCopy()
    {
        throw new NotSupportedException("Can't create a copy of a string chunk handle");
    }
}

public partial class StringChunkOwnedHandle
{
    [DllImport(ImportResolver.Library, EntryPoint = "g_string_chunk_free")]
    private static extern void Free(IntPtr chunk);

    public static partial StringChunkOwnedHandle FromUnowned(IntPtr ptr)
    {
        throw new NotSupportedException("Can't create a copy of a string chunk handle");
    }

    protected override partial bool ReleaseHandle()
    {
        Free(handle);
        return true;
    }
}
