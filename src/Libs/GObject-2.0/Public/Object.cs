﻿using System;
using System.Diagnostics;
using GObject.Internal;

namespace GObject;

public partial class Object : IDisposable
{
    public ObjectHandle Handle { get; }

    protected Object(ObjectHandle handle)
    {
        Handle = handle;
        Handle.Cache(this);
        Handle.AddMemoryPressure();
    }

    public void Dispose()
    {
        Debug.WriteLine($"Handle {Handle.DangerousGetHandle()}: Disposing object of type {GetType()}.");
        DisposeClosures();
        Handle.Dispose();
    }
}
