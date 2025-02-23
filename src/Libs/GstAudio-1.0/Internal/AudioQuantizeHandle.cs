using System;
using System.Runtime.InteropServices;

namespace GstAudio.Internal;

public partial class AudioQuantizeHandle
{
    public partial AudioQuantizeOwnedHandle OwnedCopy()
    {
        throw new NotImplementedException();
    }

    public partial AudioQuantizeUnownedHandle UnownedCopy()
    {
        throw new NotImplementedException();
    }
}

public partial class AudioQuantizeOwnedHandle
{
    [DllImport(ImportResolver.Library, EntryPoint = "gst_audio_quantize_free")]
    private static extern void Free(IntPtr data);

    public static partial AudioQuantizeOwnedHandle FromUnowned(IntPtr ptr)
    {
        throw new NotImplementedException();
    }

    protected override partial bool ReleaseHandle()
    {
        Free(handle);
        return true;
    }
}
