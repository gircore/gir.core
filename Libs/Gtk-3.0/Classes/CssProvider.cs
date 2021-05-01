using System;
using System.Runtime.InteropServices;
using System.Text;
using GLib;
using GLib.Native;
using GObject;
using GObject.Native;

namespace Gtk
{
    public partial class CssProvider
    {
        public static CssProvider New()
            => new(Native.CssProvider.Instance.Methods.New(), false);

        public bool LoadFromData(string data)
        {
            // Get as ANSI characters
            // TODO: Use ANSI or UTF-8?
            byte[] buf = Encoding.ASCII.GetBytes(data);

            // Unmanaged Call
            var error = new GLib.Native.Error.Handle(IntPtr.Zero);
            var result = Native.CssProvider.Instance.Methods.LoadFromData(Handle, buf, buf.Length, error);
            GLib.Error.ThrowOnError(error);

            // Console.WriteLine(ToString()); <-- Testing

            // Return
            return result;
        }
    }
}
