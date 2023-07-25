using System;

namespace GLib;

public partial class Dir : IDisposable
{
    public static Dir Open(string path, uint flags)
    {
        var handle = Internal.Dir.Open(Internal.NonNullableUtf8StringOwnedHandle.Create(path), flags, out var error);

        if (!error.IsInvalid)
            throw new GException(error);

        return new Dir(handle);
    }

    public void Dispose()
    {
        Internal.Dir.Close(_handle);
        _handle.Dispose();
    }
}
