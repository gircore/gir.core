using System;
using System.Runtime.InteropServices;
using System.Text;
using GLib;
using GLib.Internal;
using GObject;
using GObject.Internal;

namespace Gtk
{
    public partial class CssProvider
    {
        public static CssProvider New()
            => new(Internal.CssProvider.Instance.Methods.New(), false);

        public bool LoadFromData(string data)
        {
            // Get as ANSI characters
            // TODO: Use ANSI or UTF-8?
            byte[] buf = Encoding.ASCII.GetBytes(data);

            // Unmanaged Call
            var result = Internal.CssProvider.Instance.Methods.LoadFromData(Handle, buf, buf.Length, out var error);
            GLib.Error.ThrowOnError(error);

            // Console.WriteLine(ToString()); <-- Testing

            // Return
            return result;
        }
    }
}
