using System;
using System.Runtime.InteropServices;

namespace GstBase.Internal;

public partial class QueueArrayHandle
{
    public partial QueueArrayOwnedHandle OwnedCopy()
    {
        throw new NotImplementedException();
    }

    public partial QueueArrayUnownedHandle UnownedCopy()
    {
        throw new NotImplementedException();
    }
}

public partial class QueueArrayOwnedHandle
{
    [DllImport(ImportResolver.Library, EntryPoint = "gst_queue_array_free")]
    private static extern void Free(IntPtr array);

    public static partial QueueArrayOwnedHandle FromUnowned(IntPtr ptr)
    {
        throw new NotImplementedException();
    }

    protected override partial bool ReleaseHandle()
    {
        Free(handle);
        return true;
    }
}
