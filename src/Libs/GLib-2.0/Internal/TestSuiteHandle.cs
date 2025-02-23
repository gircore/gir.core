using System;
using System.Runtime.InteropServices;

namespace GLib.Internal;

public abstract partial class TestSuiteHandle
{
    public partial TestSuiteOwnedHandle OwnedCopy()
    {
        throw new NotSupportedException("Can't create a copy of a test suite handle");
    }

    public partial TestSuiteUnownedHandle UnownedCopy()
    {
        throw new NotSupportedException("Can't create a copy of a test suite handle");
    }
}

public partial class TestSuiteOwnedHandle
{
    [DllImport(ImportResolver.Library, EntryPoint = "g_test_suite_free")]
    private static extern void Free(IntPtr suite);

    public static partial TestSuiteOwnedHandle FromUnowned(IntPtr ptr)
    {
        throw new NotSupportedException("Can't create a copy of a test suite handle");
    }

    protected override partial bool ReleaseHandle()
    {
        Free(handle);
        return true;
    }
}
