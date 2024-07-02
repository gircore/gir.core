using System;

namespace GLib;

public sealed partial class Bytes
{
    private long _size;

    partial void Initialize()
    {
        _size = (long) Internal.Bytes.GetSize(Handle);
        GC.AddMemoryPressure(_size);
    }

    partial void OnDispose()
    {
        GC.RemoveMemoryPressure(_size);
    }
}
