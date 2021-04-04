using System;

#nullable enable

namespace GLib
{
    public static class Filename
    {
        public static string ToUri(string filename, string hostname)
        {
            var uri = Native.Functions.FilenameToUri(filename, hostname, out Native.Error.Handle error);

            Error.ThrowOnError(error);

            return uri;
        }

        public static string FromUri(string uri, out string? hostname)
        {
            var fileName = Native.Functions.FilenameFromUri(uri, out hostname, out Native.Error.Handle error);
            Error.ThrowOnError(error);

            return fileName;
        }
    }
}
