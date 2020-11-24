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
            
            return Marshal.PtrToStringAnsi(resPtr) ?? System.String.Empty;
        }

        public static string FromUri(string uri, out string hostname)
        {
            hostname = System.String.Empty;
            
            // TODO: Can we actually pass hostname as a ref string? Does this even work?
            var resPtr = Global.Native.filename_from_uri(uri, ref hostname, out IntPtr error);

            if (error != IntPtr.Zero)
                throw new GLib.GException(error);
            
            return Marshal.PtrToStringAnsi(resPtr) ?? System.String.Empty;
        }
    }
}
