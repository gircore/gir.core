using System;
using System.Runtime.InteropServices;

namespace Gst.Internal;

public partial class VecDequeHandle
{
    public partial VecDequeOwnedHandle OwnedCopy()
    {
        throw new NotImplementedException();
    }

    public partial VecDequeUnownedHandle UnownedCopy()
    {
        throw new NotImplementedException();
    }
}

public partial class VecDequeOwnedHandle
{
    [DllImport(ImportResolver.Library, EntryPoint = "gst_vec_deque_free")]
    private static extern void Free(IntPtr handle);

    public static partial VecDequeOwnedHandle FromUnowned(IntPtr ptr)
    {
        throw new NotImplementedException();
    }

    protected override partial bool ReleaseHandle()
    {
        Free(handle);
        return true;
    }
}
