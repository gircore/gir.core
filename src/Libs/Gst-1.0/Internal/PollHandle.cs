using System;
using System.Runtime.InteropServices;

namespace Gst.Internal;

public partial class PollHandle
{
    public partial PollOwnedHandle OwnedCopy()
    {
        throw new NotImplementedException();
    }

    public partial PollUnownedHandle UnownedCopy()
    {
        throw new NotImplementedException();
    }
}

public partial class PollOwnedHandle
{
    [DllImport(ImportResolver.Library, EntryPoint = "gst_poll_free")]
    private static extern void Free(IntPtr set);

    public static partial PollOwnedHandle FromUnowned(IntPtr ptr)
    {
        throw new NotImplementedException();
    }

    protected override partial bool ReleaseHandle()
    {
        Free(handle);
        return true;
    }
}
