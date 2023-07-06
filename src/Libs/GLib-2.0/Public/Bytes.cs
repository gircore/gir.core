using System;
using System.Runtime.InteropServices;

namespace GLib;

public sealed partial class Bytes : IDisposable
{
    #region Fields

    private long _size;

    #endregion

    #region Constructors

    partial void Initialize()
    {
        _size = (long) Internal.Bytes.GetSize(_handle);
        GC.AddMemoryPressure(_size);
    }

    #endregion

    #region Methods

    public static Bytes From(Span<byte> data)
    {
        var obj = new Bytes(Internal.Bytes.New(ref MemoryMarshal.GetReference(data), (nuint) data.Length));
        return obj;
    }

    public void Dispose()
    {
        _handle.Dispose();
        GC.RemoveMemoryPressure(_size);
    }

    #endregion
}
