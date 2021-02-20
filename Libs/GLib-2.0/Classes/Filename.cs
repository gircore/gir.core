using System;
using System.Runtime.InteropServices;

namespace GLib
{
    public static class Filename
    {
        public static string ToUri(string filename, string hostname)
        {
            var resPtr = Global.Native.filename_to_uri(filename, hostname, out IntPtr error);

            Error.ThrowOnError(error);

            return StringHelper.ToAnsiStringAndFree(resPtr);
        }

        public static string FromUri(string uri, out string? hostname)
        {
            // TODO: Can we actually pass hostname as a ref string? Does this even work?
            var resPtr = Global.Native.filename_from_uri(uri, out IntPtr hostnamePtr, out IntPtr error);

            Error.ThrowOnError(error);

            hostname = StringHelper.ToNullableAnsiStringAndFree(hostnamePtr);

            return StringHelper.ToAnsiStringAndFree(resPtr);
        }
    }
}
