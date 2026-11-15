using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;

namespace Gio;

public partial class Application
{
    static Application()
    {
        Module.Initialize();

        var gresources = Assembly.GetEntryAssembly()?
            .GetManifestResourceNames()
            .Where(
                name => name.EndsWith(".gresource", StringComparison.OrdinalIgnoreCase)
            )
            .ToArray();

        foreach (var gresource in gresources!)
        {
            using Stream? stream = Assembly.GetEntryAssembly()?
                .GetManifestResourceStream(gresource);

            if (stream != null)
            {
                var buffer = new byte[stream.Length];
                stream.ReadExactly(buffer);

                using var bytes = GLib.Bytes.New(buffer);
                using var resource = Resource.NewFromData(bytes);
                resource.Register();
            }
        }
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
