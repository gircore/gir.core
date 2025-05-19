using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace GObject.Internal;

public partial class ClosureOwnedHandle
{
    [DllImport(ImportResolver.Library, EntryPoint = "g_closure_invalidate")]
    private static extern void Invalidate(IntPtr closure);

    partial void OnReleaseHandle()
    {
        Debug.WriteLine($"Closure {DangerousGetHandle()}: Invalidated before release.");
        Invalidate(DangerousGetHandle());
    }
}
