using System;
using System.Diagnostics;
using GObject.Internal;

namespace GObject;

[GObject.Handle<ObjectHandle>]
public partial class Object : IDisposable, NativeObject
{
    public ObjectHandle Handle { get; }

    protected Object(ObjectHandle handle)
    {
        Handle = handle;
        Handle.AddMemoryPressure();
    }

    public virtual void Dispose()
    {
        Debug.WriteLine($"Handle {Handle.DangerousGetHandle()}: Disposing object of type {GetType()}.");
        Handle.Dispose();
    }
}
