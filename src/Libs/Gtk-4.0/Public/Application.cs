using System;
using System.Linq;
using System.Reflection;

namespace Gtk;

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

        foreach (string gresource in gresources)
        {
            using var stream = Assembly.GetEntryAssembly()
                .GetManifestResourceStream(gresource);

            if (stream != null)
            {
                var buffer = new byte[stream!.Length];
                stream.ReadExactly(buffer);

                using var bytes = GLib.Bytes.New(buffer);
                using var resource = Gio.Resource.NewFromData(bytes);
                resource.Register();
            }
        }
    }
}
