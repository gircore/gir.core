using System.Threading;

namespace Gio;

public partial class Application
{
    static Application()
    {
        Module.Initialize();
    }

    public int Run()
    {
        return Internal.Application.Run(Handle, 0, new string[0]);
    }

    public int RunWithSynchronizationContext()
    {
        var original = SynchronizationContext.Current;

        SynchronizationContext.SetSynchronizationContext(new GLib.Internal.MainLoopSynchronizationContext());

        try
        {
            return Run();
        }
        finally
        {
            SynchronizationContext.SetSynchronizationContext(original);
        }
    }
}
