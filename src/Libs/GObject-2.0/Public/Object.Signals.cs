using System;
using System.Collections.Generic;

namespace GObject;

public partial class Object
{
    private readonly Dictionary<(SignalDefinition, Delegate), (ulong, Closure)> _signalStore = new();

    internal void SignalConnectClosure(SignalDefinition signalDefinition, Delegate callback, Closure closure, bool after, string? detail)
    {
        var detailQuark = GLib.Functions.QuarkFromString(detail);
        var handlerId = Internal.Functions.SignalConnectClosureById(Handle, signalDefinition.Id, detailQuark, closure.Handle, after);

        if (handlerId == 0)
            throw new Exception($"Could not connect to event {signalDefinition.ManagedName}");

        _signalStore[(signalDefinition, callback)] = (handlerId, closure);
    }

    internal void Disconnect(SignalDefinition signalDefinition, Delegate callback)
    {
        if (!_signalStore.TryGetValue((signalDefinition, callback), out var tuple))
            return;

        Internal.Functions.SignalHandlerDisconnect(Handle, tuple.Item1);
        tuple.Item2.Dispose();
        _signalStore.Remove((signalDefinition, callback));
    }

    private void DisposeClosures()
    {
        foreach (var item in _signalStore.Values)
        {
            Internal.Functions.SignalHandlerDisconnect(Handle, item.Item1);
            item.Item2.Dispose();
        }

        _signalStore.Clear();
    }
}
