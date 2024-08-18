using System;

namespace Cairo.Internal;

public partial class SurfaceOwnedHandle
{
    private long _memoryPressure;

    internal void AddMemoryPressure(long memoryPressure)
    {
        if (memoryPressure > 0)
        {
            _memoryPressure = memoryPressure;
            GC.AddMemoryPressure(_memoryPressure);
        }
    }

    private void RemoveMemoryPressure()
    {
        if (_memoryPressure > 0)
            GC.RemoveMemoryPressure(_memoryPressure);
    }

    protected override bool ReleaseHandle()
    {
        RemoveMemoryPressure();

        Surface.Destroy(handle);
        return true;
    }
}
