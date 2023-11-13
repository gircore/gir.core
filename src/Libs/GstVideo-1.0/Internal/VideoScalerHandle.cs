using System;
using System.Runtime.InteropServices;

namespace GstVideo.Internal;

public partial class VideoScalerHandle
{
    public partial VideoScalerOwnedHandle OwnedCopy()
    {
        throw new NotImplementedException();
    }

    public partial VideoScalerUnownedHandle UnownedCopy()
    {
        throw new NotImplementedException();
    }
}

public partial class VideoScalerOwnedHandle
{
    [DllImport(ImportResolver.Library, EntryPoint = "gst_video_scaler_free")]
    private static extern void Free(IntPtr data);

    public static partial VideoScalerOwnedHandle FromUnowned(IntPtr ptr)
    {
        throw new NotImplementedException();
    }

    protected override partial bool ReleaseHandle()
    {
        Free(handle);
        return true;
    }
}
