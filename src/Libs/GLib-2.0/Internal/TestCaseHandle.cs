using System;
using System.Runtime.InteropServices;

namespace GLib.Internal;

public abstract partial class TestCaseHandle
{
    public partial TestCaseOwnedHandle OwnedCopy()
    {
        throw new NotSupportedException("Can't create a copy of a test case handle");
    }

    public partial TestCaseUnownedHandle UnownedCopy()
    {
        throw new NotSupportedException("Can't create a copy of a test case handle");
    }
}

public partial class TestCaseOwnedHandle
{
    [DllImport(ImportResolver.Library, EntryPoint = "g_test_case_free")]
    private static extern void Free(IntPtr testCase);

    public static partial TestCaseOwnedHandle FromUnowned(IntPtr ptr)
    {
        throw new NotSupportedException("Can't create a copy of a test case handle");
    }

    protected override partial bool ReleaseHandle()
    {
        Free(handle);
        return true;
    }
}
