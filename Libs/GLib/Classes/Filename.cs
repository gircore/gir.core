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
            
            return Marshal.PtrToStringAnsi(resPtr) ?? string.Empty;
        }

        public static string FromUri(string uri, out string? hostname)
        {
            IntPtr hostnamePtr = default;
            
            // TODO: Can we actually pass hostname as a ref string? Does this even work?
            var resPtr = Global.Native.filename_from_uri(uri, out hostnamePtr, out IntPtr error);

            if (error != IntPtr.Zero)
                throw new GLib.GException(error);

            hostname = Marshal.PtrToStringAnsi(hostnamePtr) ?? null;
            
            return Marshal.PtrToStringAnsi(resPtr) ?? string.Empty;
        }
    }
}
