using System;
using System.Runtime.InteropServices;

namespace Gtk.Internal;

public abstract partial class BuildableParseContextHandle
{
    [DllImport(ImportResolver.Library, EntryPoint = "girtest_opaque_untyped_record_tester_ref")]
    internal static extern IntPtr Ref(IntPtr queue);

    public partial BuildableParseContextOwnedHandle OwnedCopy()
    {
        throw new NotSupportedException("Can't create a copy of this handle");
    }

    public partial BuildableParseContextUnownedHandle UnownedCopy()
    {
        throw new NotSupportedException("Can't create a copy of this handle");
    }
}

public partial class BuildableParseContextOwnedHandle
{
    public static partial BuildableParseContextOwnedHandle FromUnowned(IntPtr ptr)
    {
        throw new NotSupportedException("Can't create a copy of this handle");
    }

    protected override partial bool ReleaseHandle()
    {
        throw new NotSupportedException("Can't create a copy of this handle");
    }
}
