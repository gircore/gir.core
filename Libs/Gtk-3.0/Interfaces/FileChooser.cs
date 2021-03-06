using GLib;

namespace Gtk
{
    public partial interface FileChooser
    {
        public string? GetUri()
        {
            var obj = (GObject.Object) this;
            return Native.Methods.GetUri(obj.Handle);
        }
    }
}
