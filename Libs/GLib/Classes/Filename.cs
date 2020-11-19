using System;
using System.Runtime.InteropServices;

namespace GLib
{
    public static class Filename
    {
        public static string ToUri(string filename, string hostname, out Error? error)
        {
            error = null;

            var resPtr = Global.Native.filename_to_uri(filename, hostname, out IntPtr errPtr);

            if (errPtr != null)
                error = Marshal.PtrToStructure<Error>(errPtr);

            return Marshal.PtrToStringAnsi(resPtr);
        }

        public static string FromUri(string uri, string hostname, out Error? error)
        {
            error = null;

            var resPtr = Global.Native.filename_from_uri(uri, hostname, out IntPtr errPtr);

            if (errPtr != null)
                error = Marshal.PtrToStructure<Error>(errPtr);

            return Marshal.PtrToStringAnsi(resPtr);
        }
    }
}
