using System;
using GLib;

namespace GdkPixbuf;

public partial class Pixbuf
{
    #region Fields

    private long _size;

    #endregion

    public static Pixbuf NewFromFile(string fileName)
    {
        IntPtr handle = Internal.Pixbuf.NewFromFile(fileName, out var error);
        Error.ThrowOnError(error);

        return new Pixbuf(handle, true);
    }

    protected override void Initialize()
    {
        base.Initialize();
        _size = (long) Internal.Pixbuf.GetByteLength(Handle);
        GC.AddMemoryPressure(_size);
    }

    public override void Dispose()
    {
        base.Dispose();
        GC.RemoveMemoryPressure(_size);
    }
}
