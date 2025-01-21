using System;

namespace GdkPixbuf.Internal;

public partial class PixbufHandle
{
    private long _size;

    protected override void AddMemoryPressure()
    {
        _size = (long) Pixbuf.GetByteLength(handle);
        GC.AddMemoryPressure(_size);
    }

    protected override void RemoveMemoryPressure()
    {
        GC.RemoveMemoryPressure(_size);
    }
}
