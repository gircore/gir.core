using System;
using System.Runtime.InteropServices;

namespace GLib;

public partial class VariantType : IDisposable
{
    public static readonly VariantType String = New("s");
    public static readonly VariantType Variant = New("v");

    public override string ToString()
        => Internal.VariantType.PeekString(Handle).ConvertToString();

    public void Dispose()
        => Handle.Dispose();
}
