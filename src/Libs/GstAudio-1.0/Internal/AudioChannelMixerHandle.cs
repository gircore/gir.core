using System;
using System.Runtime.InteropServices;

namespace GstAudio.Internal;

public partial class AudioChannelMixerHandle
{
    public partial AudioChannelMixerOwnedHandle OwnedCopy()
    {
        throw new NotImplementedException();
    }

    public partial AudioChannelMixerUnownedHandle UnownedCopy()
    {
        throw new NotImplementedException();
    }
}

public partial class AudioChannelMixerOwnedHandle
{
    [DllImport(ImportResolver.Library, EntryPoint = "gst_audio_channel_mixer_free")]
    private static extern void Free(IntPtr data);

    public static partial AudioChannelMixerOwnedHandle FromUnowned(IntPtr ptr)
    {
        throw new NotImplementedException();
    }

    protected override partial bool ReleaseHandle()
    {
        Free(handle);
        return true;
    }
}
