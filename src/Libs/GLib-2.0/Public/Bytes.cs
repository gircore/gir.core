using System;
using System.Runtime.CompilerServices;

namespace GLib;

public partial class Bytes
{
    public ReadOnlySpan<T> GetRegionSpan<T>(nuint offset, nuint count) where T : unmanaged
    {
        unsafe
        {
            var region = GetRegion((nuint) Unsafe.SizeOf<T>(), offset, count);

            return region == IntPtr.Zero
                ? ReadOnlySpan<T>.Empty
                : new ReadOnlySpan<T>((void*) region, (int) count);
        }
    }
}
