using System.Runtime.InteropServices;

namespace Cairo;

[StructLayout(LayoutKind.Sequential)]
public struct TextExtents
{
    public double XBearing;
    public double YBearing;
    public double Width;
    public double Height;
    public double XAdvance;
    public double YAdvance;
}
