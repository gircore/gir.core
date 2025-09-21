using System;
using System.Runtime.InteropServices;

namespace Cairo.Internal;

public class UserDataHandler
{
    private readonly SafeHandle _data;
    private GCHandle _gch;
    
    public readonly GLib.Internal.DestroyNotify DestroyNotify;
    public IntPtr Key => GCHandle.ToIntPtr(_gch);

    public UserDataHandler(SafeHandle data)
    {
        _data = data; //Keep data alive
        _gch = GCHandle.Alloc(this);
        
        DestroyNotify = DestroyCallback;
    }

    private void DestroyCallback(IntPtr userData)
    {
        _gch.Free();
    }
}