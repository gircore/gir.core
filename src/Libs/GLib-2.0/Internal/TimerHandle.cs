using System;
using System.Runtime.InteropServices;

namespace GLib.Internal;

public abstract partial class TimerHandle
{
    public partial TimerOwnedHandle OwnedCopy()
    {
        throw new NotSupportedException("Can't create a copy of a timer handle");
    }

    public partial TimerUnownedHandle UnownedCopy()
    {
        throw new NotSupportedException("Can't create a copy of a timer handle");
    }
}

public partial class TimerOwnedHandle
{
    [DllImport(ImportResolver.Library, EntryPoint = "g_timer_destroy")]
    private static extern void Destroy(IntPtr timer);

    public static partial TimerOwnedHandle FromUnowned(IntPtr ptr)
    {
        throw new NotSupportedException("Can't create a copy of a timer handle");
    }

    protected override partial bool ReleaseHandle()
    {
        Destroy(handle);
        return true;
    }
}
