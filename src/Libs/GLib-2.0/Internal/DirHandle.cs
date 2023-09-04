using System;
using System.Runtime.InteropServices;

namespace GLib.Internal;

public abstract partial class DirHandle
{
    public partial DirOwnedHandle OwnedCopy()
    {
        throw new NotSupportedException("Can't create a copy of a directory handle");
    }

    public partial DirUnownedHandle UnownedCopy()
    {
        throw new NotSupportedException("Can't create a copy of a directory handle");
    }
}

public partial class DirOwnedHandle
{
    [DllImport(ImportResolver.Library, EntryPoint = "g_dir_close")]
    private static extern void Close(IntPtr dir);

    public static partial DirOwnedHandle FromUnowned(IntPtr ptr)
    {
        throw new NotSupportedException("Can't create a copy of a directory handle");
    }

    protected override partial bool ReleaseHandle()
    {
        Close(handle);
        return true;
    }
}
