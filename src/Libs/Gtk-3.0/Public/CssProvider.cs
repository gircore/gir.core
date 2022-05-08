using System.Text;

namespace Gtk
{
    public partial class CssProvider
    {
        public bool LoadFromData(string data)
        {
            // Get as ANSI characters
            // TODO: Use ANSI or UTF-8?
            byte[] buf = Encoding.ASCII.GetBytes(data);

            // Unmanaged Call
            var result = Internal.CssProvider.LoadFromData(Handle, buf, buf.Length, out var error);
            GLib.Error.ThrowOnError(error);

            // Console.WriteLine(ToString()); <-- Testing

            // Return
            return result;
        }
    }
}
