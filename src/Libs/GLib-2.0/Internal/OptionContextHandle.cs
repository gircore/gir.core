using System;
using System.Runtime.InteropServices;

namespace GLib.Internal;

public abstract partial class OptionContextHandle
{
    public partial OptionContextOwnedHandle OwnedCopy()
    {
        throw new NotSupportedException("Can't create a copy of an option context handle");
    }

    public partial OptionContextUnownedHandle UnownedCopy()
    {
        throw new NotSupportedException("Can't create a copy of an option context handle");
    }
}

public partial class OptionContextOwnedHandle
{
    [DllImport(ImportResolver.Library, EntryPoint = "g_option_context_free")]
    private static extern void Free(IntPtr context);

    public static partial OptionContextOwnedHandle FromUnowned(IntPtr ptr)
    {
        throw new NotSupportedException("Can't create a copy of an option context handle");
    }

    protected override partial bool ReleaseHandle()
    {
        Free(handle);
        return true;
    }
}
