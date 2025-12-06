using System;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;

namespace Win32;

[UnsupportedOSPlatform("Linux")]
[UnsupportedOSPlatform("OSX")]
[StructLayout(LayoutKind.Explicit)]
public readonly struct HCURSOR(IntPtr value)
{
    [FieldOffset(0)]
    private readonly IntPtr _value = value;

    public static implicit operator IntPtr(HCURSOR o) => o._value;
    public static implicit operator HCURSOR(IntPtr o) => new(o);
}
