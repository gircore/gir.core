using System;
using System.Runtime.InteropServices;

namespace GLib;

public partial class VariantType : IDisposable
{
    public static readonly VariantType String = New("s");
    public static readonly VariantType Variant = New("v");

    public void Dispose()
    {
        Handle.Dispose();
    }
}
