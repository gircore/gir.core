using System;

namespace GLib;

public partial class Source
{
    public void SetCallback(SourceFunc sourceFunc)
    {
        var handler = new Internal.SourceFuncNotifiedHandler(sourceFunc);
        Internal.Source.SetCallback(Handle, handler.NativeCallback, IntPtr.Zero, handler.DestroyNotify);
    }

    public void Attach(MainContext mainContext)
    {
        Internal.Source.Attach(Handle, mainContext.Handle);
    }

    public static void Remove(uint tag) => Internal.Functions.SourceRemove(tag);
}
