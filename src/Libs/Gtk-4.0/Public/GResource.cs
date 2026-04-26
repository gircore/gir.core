using Gio;

namespace Gtk;

public class GResource : TemplateLoader
{
    public static GLib.Bytes Load(string resourceName)
    {
        File file = Gio.Functions.FileNewForUri($"resource://{resourceName}");
        return file.LoadBytes(null, out _);
    }
}
