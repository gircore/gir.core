using System;
using System.Runtime.InteropServices;

namespace GLib.Internal;

public abstract partial class StrvBuilderHandle
{
    [DllImport(ImportResolver.Library, EntryPoint = "g_strv_builder_ref")]
    protected static extern IntPtr Ref(IntPtr builder);

    public partial StrvBuilderOwnedHandle OwnedCopy()
    {
        return new StrvBuilderOwnedHandle(Ref(handle));
    }

    public partial StrvBuilderUnownedHandle UnownedCopy()
    {
        return new StrvBuilderUnownedHandle(Ref(handle));
    }
}

public partial class StrvBuilderOwnedHandle
{
    [DllImport(ImportResolver.Library, EntryPoint = "g_strv_builder_unref")]
    private static extern void Unref(IntPtr builder);

    public static partial StrvBuilderOwnedHandle FromUnowned(IntPtr ptr)
    {
        return new StrvBuilderOwnedHandle(Ref(ptr));
    }

    protected override partial bool ReleaseHandle()
    {
        Unref(handle);
        return true;
    }
}
