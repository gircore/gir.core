using System;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;

namespace Win32;

[UnsupportedOSPlatform("Linux")]
[UnsupportedOSPlatform("OSX")]
[StructLayout(LayoutKind.Explicit)]
public readonly struct Resource(IntPtr value)
{
    [FieldOffset(0)]
    private readonly IntPtr _value = value;

    public static implicit operator IntPtr(Resource o) => o._value;
    public static implicit operator Resource(IntPtr o) => new(o);
}
