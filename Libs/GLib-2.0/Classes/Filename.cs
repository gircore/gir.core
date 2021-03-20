using System;

#nullable enable

namespace GLib
{
    public static class Filename
    {
        public static string ToUri(string filename, string hostname)
        {
            var uri = Functions.Native.FilenameToUri(filename, hostname, out Error.Native.ErrorSafeHandle error);

            Error.ThrowOnError(error);

            return uri;
        }

        public static string FromUri(string uri, out string? hostname)
        {
            var fileName = Functions.Native.FilenameFromUri(uri, out hostname, out Error.Native.ErrorSafeHandle error);
            Error.ThrowOnError(error);

            return fileName;
        }
    }
}
