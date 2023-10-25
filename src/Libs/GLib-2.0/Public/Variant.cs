using System;
using GLib.Internal;

namespace GLib;

public partial class Variant : IDisposable
{
    public void Dispose()
    {
        Handle.Dispose();
    }
}
