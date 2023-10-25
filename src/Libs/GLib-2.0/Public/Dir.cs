using System;

namespace GLib;

public partial class Dir : IDisposable
{
    public void Dispose()
    {
        Handle.Dispose();
    }
}
