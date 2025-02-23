using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace GObject;

public partial class Object
{
    private readonly Dictionary<(SignalDefinition, Delegate), (CULong, Closure)> _signalStore = new();

    internal void SignalConnectClosure(SignalDefinition signalDefinition, Delegate callback, Closure closure, bool after, string? detail)
    {
        var detailQuark = GLib.Functions.QuarkFromString(detail);
        var handlerId = Internal.Functions.SignalConnectClosureById(Handle.DangerousGetHandle(), signalDefinition.Id, detailQuark, closure.Handle, after);

        if (handlerId.Value == 0)
            throw new Exception($"Could not connect to event {signalDefinition.ManagedName}");

        _signalStore[(signalDefinition, callback)] = (handlerId, closure);
    }

    internal void Disconnect(SignalDefinition signalDefinition, Delegate callback)
    {
        if (!_signalStore.TryGetValue((signalDefinition, callback), out var tuple))
            return;

        Internal.Functions.SignalHandlerDisconnect(Handle.DangerousGetHandle(), tuple.Item1);
        tuple.Item2.Dispose();
        _signalStore.Remove((signalDefinition, callback));
    }

    private void DisposeClosures()
    {
        foreach (var item in _signalStore.Values)
        {
            Internal.Functions.SignalHandlerDisconnect(Handle.DangerousGetHandle(), item.Item1);
            item.Item2.Dispose();
        }

        _signalStore.Clear();
    }
}
