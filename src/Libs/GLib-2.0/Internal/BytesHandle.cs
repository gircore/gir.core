using System;

namespace GLib.Internal;

public partial class BytesOwnedHandle
{
    private long _pressure;

    partial void AddMemoryPressure()
    {
        _pressure = (long) Bytes.GetSize(this);
        GC.AddMemoryPressure(_pressure);
    }

    partial void RemoveMemoryPressure()
    {
        GC.RemoveMemoryPressure(_pressure);
    }
}
