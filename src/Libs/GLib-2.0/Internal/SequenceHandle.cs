using System;
using System.Runtime.InteropServices;

namespace GLib.Internal;

public abstract partial class SequenceHandle
{
    public partial SequenceOwnedHandle OwnedCopy()
    {
        throw new NotSupportedException("Can't create a copy of a sequence handle");
    }

    public partial SequenceUnownedHandle UnownedCopy()
    {
        throw new NotSupportedException("Can't create a copy of a sequence handle");
    }
}

public partial class SequenceOwnedHandle : SequenceHandle
{
    [DllImport(ImportResolver.Library, EntryPoint = "g_sequence_free")]
    private static extern void Free(IntPtr seq);

    public static partial SequenceOwnedHandle FromUnowned(IntPtr ptr)
    {
        throw new NotSupportedException("Can't create a copy of a sequence handle");
    }

    protected override partial bool ReleaseHandle()
    {
        Free(handle);
        return true;
    }
}
