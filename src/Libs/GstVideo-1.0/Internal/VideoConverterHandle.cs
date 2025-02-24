using System;
using System.Runtime.InteropServices;

namespace GstVideo.Internal;

public partial class VideoConverterHandle
{
    public partial VideoConverterOwnedHandle OwnedCopy()
    {
        throw new NotImplementedException();
    }

    public partial VideoConverterUnownedHandle UnownedCopy()
    {
        throw new NotImplementedException();
    }
}

public partial class VideoConverterOwnedHandle
{
    [DllImport(ImportResolver.Library, EntryPoint = "gst_video_converter_free")]
    private static extern void Free(IntPtr data);

    public static partial VideoConverterOwnedHandle FromUnowned(IntPtr ptr)
    {
        throw new NotImplementedException();
    }

    protected override partial bool ReleaseHandle()
    {
        Free(handle);
        return true;
    }
}
