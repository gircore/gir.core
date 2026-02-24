namespace Gtk;

public interface TemplateLoader
{
    static abstract GLib.Bytes Load(string resourceName);
}
