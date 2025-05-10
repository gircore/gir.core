using System.Diagnostics;

namespace GObject.Internal;

public partial class ClosureOwnedHandle
{
    partial void OnReleaseHandle()
    {
        Debug.WriteLine($"Closure {DangerousGetHandle()}: Reference will be released.");
    }
}
