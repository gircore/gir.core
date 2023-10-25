using System;
using System.Runtime.InteropServices;

namespace GLib.Internal;

public abstract partial class VariantHandle
{
    [DllImport(ImportResolver.Library, EntryPoint = "g_variant_ref_sink")]
    protected static extern IntPtr RefSink(IntPtr value);

    public partial VariantOwnedHandle OwnedCopy()
    {
        return new VariantOwnedHandle(RefSink(handle));
    }

    public partial VariantUnownedHandle UnownedCopy()
    {
        return new VariantUnownedHandle(RefSink(handle));
    }
}

public partial class VariantOwnedHandle
{
    [DllImport(ImportResolver.Library, EntryPoint = "g_variant_unref")]
    private static extern void Unref(IntPtr value);

    public static partial VariantOwnedHandle FromUnowned(IntPtr ptr)
    {
        return new VariantOwnedHandle(RefSink(ptr));
    }

    protected override partial bool ReleaseHandle()
    {
        Unref(handle);
        return true;
    }
}
