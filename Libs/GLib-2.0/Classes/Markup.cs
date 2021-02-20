using System;
using System.Runtime.InteropServices;
using System.Text;

namespace GLib
{
    public static class Markup
    {
        public static string EscapeText(string text)
        {
            var numBytes = Encoding.UTF8.GetByteCount(text);
            IntPtr ret = Global.Native.markup_escape_text(text, numBytes);

            return Marshal.PtrToStringAuto(ret) ?? System.String.Empty;
        }
    }
}
