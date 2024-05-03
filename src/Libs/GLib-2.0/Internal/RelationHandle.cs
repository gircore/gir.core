using System;

namespace GLib.Internal;

public partial class RelationHandle
{
    public partial RelationOwnedHandle OwnedCopy()
    {
        throw new NotSupportedException($"Can't create a copy of a {nameof(RelationHandle)}.");
    }

    public partial RelationUnownedHandle UnownedCopy()
    {
        throw new NotSupportedException($"Can't create a copy of a {nameof(RelationHandle)}.");
    }
}

public partial class RelationOwnedHandle
{
    public static partial RelationOwnedHandle FromUnowned(IntPtr ptr)
    {
        throw new NotSupportedException($"Can't create a copy of a {nameof(RelationOwnedHandle)}.");
    }

    protected override partial bool ReleaseHandle()
    {
        throw new NotSupportedException($"Can't free a {nameof(RelationOwnedHandle)}.");
    }
}
