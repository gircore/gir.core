using System;

#nullable enable

namespace GLib
{
    public static class Filename
    {
        public static string ToUri(string filename, string hostname)
        {
            var error = new Native.Error.Handle(IntPtr.Zero);
            var uri = Native.Functions.FilenameToUri(filename, hostname, error);

            Error.ThrowOnError(error);
            error.Dispose();

            return uri;
        }

        public static string FromUri(string uri, out string? hostname)
        {
            var error = new Native.Error.Handle(IntPtr.Zero);
            var fileName = Native.Functions.FilenameFromUri(uri, out hostname, error);
            Error.ThrowOnError(error);
            error.Dispose();

            return fileName;
        }
    }
}
