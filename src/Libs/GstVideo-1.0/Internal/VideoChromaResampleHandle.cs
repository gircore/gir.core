using System;
using System.Runtime.InteropServices;

namespace GstVideo.Internal;

public partial class VideoChromaResampleHandle
{
    public partial VideoChromaResampleOwnedHandle OwnedCopy()
    {
        throw new NotImplementedException();
    }

    public partial VideoChromaResampleUnownedHandle UnownedCopy()
    {
        throw new NotImplementedException();
    }
}

public partial class VideoChromaResampleOwnedHandle
{
    [DllImport(ImportResolver.Library, EntryPoint = "gst_video_chroma_resample_free")]
    private static extern void Free(IntPtr data);

    public static partial VideoChromaResampleOwnedHandle FromUnowned(IntPtr ptr)
    {
        throw new NotImplementedException();
    }

    protected override partial bool ReleaseHandle()
    {
        Free(handle);
        return true;
    }
}
