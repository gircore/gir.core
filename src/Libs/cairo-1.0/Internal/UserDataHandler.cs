using System;
using System.Runtime.InteropServices;

namespace Cairo.Internal;

public class UserDataHandler
{
#pragma warning disable IDE0052
    private readonly SafeHandle _data; //Keep data alive
#pragma warning restore IDE0052

    private GCHandle _gch;

    public readonly GLib.Internal.DestroyNotify DestroyNotify;

    public UserDataHandler(SafeHandle data)
    {
        _data = data;
        _gch = GCHandle.Alloc(this);

        DestroyNotify = DestroyCallback;
    }

    private void DestroyCallback(IntPtr userData)
    {
        _gch.Free();
    }
}
