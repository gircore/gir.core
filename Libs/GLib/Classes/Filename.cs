using System;
using System.Runtime.InteropServices;

namespace GLib
{
    public static class Filename
    {
        public static string ToUri(string filename, string hostname)
        {
            var resPtr = Global.Native.filename_to_uri(filename, hostname, out IntPtr error);

            if (error != IntPtr.Zero)
                throw new GLib.GException(error);

            return Marshal.PtrToStringAnsi(resPtr);
        }

        public static string FromUri(string uri, string hostname)
        {
            var resPtr = Global.Native.filename_from_uri(uri, hostname, out IntPtr error);

            if (error != IntPtr.Zero)
                throw new GLib.GException(error);

            return Marshal.PtrToStringAnsi(resPtr);
        }
    }
}
