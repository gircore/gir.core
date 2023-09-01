using System.Threading;

namespace GLib;

public sealed partial class MainLoop
{
    public void RunWithSynchronizationContext()
    {
        var original = SynchronizationContext.Current;

        SynchronizationContext.SetSynchronizationContext(new Internal.MainLoopSynchronizationContext());

        try
        {
            Run();
        }
        finally
        {
            SynchronizationContext.SetSynchronizationContext(original);
        }
    }
}
