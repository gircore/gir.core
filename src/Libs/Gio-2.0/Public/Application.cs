using System;
using System.Threading;

namespace Gio;

public partial class Application
{
    static Application()
    {
        Module.Initialize();
    }

    public int RunWithSynchronizationContext(string[]? args)
    {
        var original = SynchronizationContext.Current;

        SynchronizationContext.SetSynchronizationContext(new GLib.Internal.MainLoopSynchronizationContext());

        try
        {
            return Run(args);
        }
        finally
        {
            SynchronizationContext.SetSynchronizationContext(original);
        }
    }
}
