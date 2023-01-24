using System;

namespace GLib;

public partial class Dir : IDisposable
{
    public static Dir Open(string path, uint flags)
    {
        var handle = Internal.Dir.Open(path, flags, out var error);
        Error.ThrowOnError(error);

        return new Dir(handle);
    }

    public void Dispose()
    {
        Internal.Dir.Close(_handle);
        _handle.Dispose();
    }
}
