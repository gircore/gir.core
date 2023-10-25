using System;
using System.Runtime.InteropServices;

namespace GLib.Internal;

public abstract partial class RandHandle
{
    public partial RandOwnedHandle OwnedCopy()
    {
        throw new NotSupportedException("Can't create a copy of a rand handle");
    }

    public partial RandUnownedHandle UnownedCopy()
    {
        throw new NotSupportedException("Can't create a copy of a rand handle");
    }
}

public partial class RandOwnedHandle
{
    [DllImport(ImportResolver.Library, EntryPoint = "g_rand_free")]
    private static extern void Free(IntPtr rand);

    public static partial RandOwnedHandle FromUnowned(IntPtr ptr)
    {
        throw new NotSupportedException("Can't create a copy of a rand handle");
    }

    protected override partial bool ReleaseHandle()
    {
        Free(handle);
        return true;
    }
}
