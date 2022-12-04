namespace GLib;

public partial class Dir
{
    public static Dir Open(string path, uint flags)
    {
        var handle = Internal.Dir.Open(path, flags, out var error);
        Error.ThrowOnError(error);

        return new Dir(handle);
    }
}
