using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.InteropServices;

namespace GObject.Internal;

public class ObjectHandle : SafeHandle
{
    private readonly Dictionary<Delegate, GObject.Closure> closures = [];

    public override bool IsInvalid => handle == IntPtr.Zero;

    public ObjectHandle(IntPtr handle) : base(IntPtr.Zero, true)
    {
        SetHandle(handle);
    }

    internal GObject.Closure GetClosure(Delegate signalHandler, Func<GObject.Closure> createClosure)
    {
        if (closures.TryGetValue(signalHandler, out var closure))
            return closure;

        closure = createClosure();
        closures.Add(signalHandler, closure);

        return closure;
    }

    internal bool TryGetClosure(Delegate signalHandler, [NotNullWhen(true)] out GObject.Closure? closure)
    {
        return closures.TryGetValue(signalHandler, out closure);
    }

    protected override bool ReleaseHandle()
    {
        RemoveMemoryPressure();
        InstanceCache.Remove(handle);
        return true;
    }

    protected internal virtual void AddMemoryPressure() { }
    protected virtual void RemoveMemoryPressure() { }
}
