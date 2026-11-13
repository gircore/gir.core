using System;
using System.Runtime.InteropServices;

namespace Gst.Internal;

public partial class LogContextHandle
{
    public partial LogContextOwnedHandle OwnedCopy()
    {
        throw new NotImplementedException();
    }

    public partial LogContextUnownedHandle UnownedCopy()
    {
        throw new NotImplementedException();
    }
}

public partial class LogContextOwnedHandle
{
    [LibraryImport(ImportResolver.Library, EntryPoint = "gst_log_context_free")]
    private static partial void Free(IntPtr set);

    public static partial LogContextOwnedHandle FromUnowned(IntPtr ptr)
    {
        throw new NotImplementedException();
    }

    protected override partial bool ReleaseHandle()
    {
        Free(handle);
        return true;
    }
}
