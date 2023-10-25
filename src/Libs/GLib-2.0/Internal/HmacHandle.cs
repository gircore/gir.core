using System;
using System.Runtime.InteropServices;

namespace GLib.Internal;

public abstract partial class HmacHandle
{
    [DllImport(ImportResolver.Library, EntryPoint = "g_hmac_ref")]
    protected static extern IntPtr Ref(IntPtr hmac);

    public partial HmacOwnedHandle OwnedCopy()
    {
        return new HmacOwnedHandle(Ref(handle)); ;
    }

    public partial HmacUnownedHandle UnownedCopy()
    {
        return new HmacUnownedHandle(Ref(handle));
    }
}

public partial class HmacOwnedHandle
{
    [DllImport(ImportResolver.Library, EntryPoint = "g_hmac_unref")]
    private static extern void Unref(IntPtr hmac);

    public static partial HmacOwnedHandle FromUnowned(IntPtr ptr)
    {
        return new HmacOwnedHandle(Ref(ptr));
    }

    protected override partial bool ReleaseHandle()
    {
        Unref(handle);
        return true;
    }
}
