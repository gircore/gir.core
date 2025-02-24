using System;
using System.Runtime.InteropServices;

namespace GstAudio.Internal;

public partial class AudioResamplerHandle
{
    public partial AudioResamplerOwnedHandle OwnedCopy()
    {
        throw new NotImplementedException();
    }

    public partial AudioResamplerUnownedHandle UnownedCopy()
    {
        throw new NotImplementedException();
    }
}

public partial class AudioResamplerOwnedHandle
{
    [DllImport(ImportResolver.Library, EntryPoint = "gst_audio_resampler_free")]
    private static extern void Free(IntPtr data);

    public static partial AudioResamplerOwnedHandle FromUnowned(IntPtr ptr)
    {
        throw new NotImplementedException();
    }

    protected override partial bool ReleaseHandle()
    {
        Free(handle);
        return true;
    }
}
