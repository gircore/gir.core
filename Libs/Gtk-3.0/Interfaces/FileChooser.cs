using GLib;

namespace Gtk
{
    public partial interface FileChooser
    {
        public string? GetUri()
        {
            var obj = (GObject.Object) this;
            var ptr = Native.get_uri(obj.Handle);
            return StringHelper.ToAnsiStringAndFree(ptr);
        }
    }
}
