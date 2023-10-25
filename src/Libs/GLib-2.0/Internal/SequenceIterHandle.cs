using System;

namespace GLib.Internal;

public abstract partial class SequenceIterHandle
{
    public partial SequenceIterOwnedHandle OwnedCopy()
    {
        throw new NotImplementedException();
    }

    public partial SequenceIterUnownedHandle UnownedCopy()
    {
        throw new NotImplementedException();
    }
}

public partial class SequenceIterOwnedHandle
{
    public static partial SequenceIterOwnedHandle FromUnowned(IntPtr ptr)
    {
        throw new NotImplementedException();
    }

    protected override partial bool ReleaseHandle()
    {
        throw new NotImplementedException();
    }
}
