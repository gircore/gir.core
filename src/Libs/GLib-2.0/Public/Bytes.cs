using System;
using System.Runtime.InteropServices;

namespace GLib;

public sealed partial class Bytes : IDisposable
{
    private long _size;

    partial void Initialize()
    {
        _size = (long) Internal.Bytes.GetSize(Handle);
        GC.AddMemoryPressure(_size);
    }

    public void Dispose()
    {
        Handle.Dispose();
        GC.RemoveMemoryPressure(_size);
    }
}
