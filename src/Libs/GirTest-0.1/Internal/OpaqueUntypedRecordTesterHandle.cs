using System;
using System.Runtime.InteropServices;

namespace GirTest.Internal;

public abstract partial class OpaqueUntypedRecordTesterHandle
{
    [DllImport(ImportResolver.Library, EntryPoint = "girtest_opaque_untyped_record_tester_ref")]
    protected static extern IntPtr Ref(IntPtr queue);

    public partial OpaqueUntypedRecordTesterOwnedHandle OwnedCopy()
    {
        return new OpaqueUntypedRecordTesterOwnedHandle(Ref(handle));
    }

    public partial OpaqueUntypedRecordTesterUnownedHandle UnownedCopy()
    {
        return new OpaqueUntypedRecordTesterUnownedHandle(Ref(handle));
    }
}

public partial class OpaqueUntypedRecordTesterOwnedHandle
{
    [DllImport(ImportResolver.Library, EntryPoint = "girtest_opaque_untyped_record_tester_unref")]
    private static extern void Unref(IntPtr queue);

    public static partial OpaqueUntypedRecordTesterOwnedHandle FromUnowned(IntPtr ptr)
    {
        return new OpaqueUntypedRecordTesterOwnedHandle(Ref(ptr));
    }

    protected override partial bool ReleaseHandle()
    {
        Unref(handle);
        return true;
    }
}
