using System;
using System.Runtime.InteropServices;

namespace GstBase.Internal;

public partial class TypeFindDataHandle
{
    public partial TypeFindDataOwnedHandle OwnedCopy()
    {
        throw new NotImplementedException();
    }

    public partial TypeFindDataUnownedHandle UnownedCopy()
    {
        throw new NotImplementedException();
    }
}

public partial class TypeFindDataOwnedHandle
{
    [DllImport(ImportResolver.Library, EntryPoint = "gst_type_find_data_free")]
    private static extern void Free(IntPtr data);

    public static partial TypeFindDataOwnedHandle FromUnowned(IntPtr ptr)
    {
        throw new NotImplementedException();
    }

    protected override partial bool ReleaseHandle()
    {
        Free(handle);
        return true;
    }
}
