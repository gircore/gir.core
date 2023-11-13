using System;
using System.Runtime.InteropServices;

namespace GstVideo.Internal;

public partial class VideoDitherHandle
{
    public partial VideoDitherOwnedHandle OwnedCopy()
    {
        throw new NotImplementedException();
    }

    public partial VideoDitherUnownedHandle UnownedCopy()
    {
        throw new NotImplementedException();
    }
}

public partial class VideoDitherOwnedHandle
{
    [DllImport(ImportResolver.Library, EntryPoint = "gst_video_dither_free")]
    private static extern void Free(IntPtr data);

    public static partial VideoDitherOwnedHandle FromUnowned(IntPtr ptr)
    {
        throw new NotImplementedException();
    }

    protected override partial bool ReleaseHandle()
    {
        Free(handle);
        return true;
    }
}
