using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;

namespace GObject;

public delegate void ClosureCallback(Value? returnValue, Value[] paramValues);

public partial class Closure
{
    private readonly ClosureCallback _callback;
    private readonly Internal.ClosureMarshal _closureMarshal; //Needed to keep delegate alive
    private readonly Dictionary<(IntPtr, uint), Queue<CULong>> _signalHandlers = new();

    internal Closure(ClosureCallback callback)
    {
        _callback = callback;
        //The initial state is floating (meaning there is already a ref that is unowned).
        //So we first create an owned copy (to ref the instance), which increases the count to 2.
        //Afterward sink is called, which might decrement the reference count again by 1 if the instance
        //is not yet sunk. See: https://docs.gtk.org/gobject/method.Closure.sink.html
        Handle = Internal.Closure
            .NewSimple((uint) Marshal.SizeOf<Internal.ClosureData>(), IntPtr.Zero)
            .OwnedCopy();

        Debug.WriteLine($"Closure {Handle.DangerousGetHandle()}: Created");

        _closureMarshal = InternalCallback; //Save delegate to keep the instance alive

        Internal.Closure.Sink(Handle);
        Internal.Closure.SetMarshal(Handle, _closureMarshal);
    }

    private void InternalCallback(IntPtr closure, IntPtr returnValuePtr, uint nParamValues, IntPtr paramValuesData, IntPtr invocationHint, IntPtr userData)
    {
        var returnUnownedHandle = new Internal.ValueUnownedHandle(returnValuePtr);
        var returnValue = returnValuePtr != IntPtr.Zero
            ? new Value(returnUnownedHandle.OwnedCopy())
            : null;

        var paramValues = new Internal.ValueArray2UnownedHandle(paramValuesData, (int) nParamValues).ToArray((int) nParamValues);

        try
        {
            _callback(returnValue, paramValues);

            //The result must be copied back into the original value as the callback operates on a copy
            if (returnValue is not null && !returnUnownedHandle.IsInvalid)
                Internal.Value.Copy(returnValue.Handle, returnUnownedHandle);
        }
        catch (Exception e)
        {
            GLib.UnhandledException.Raise(e);
        }
    }

    public void Connect(Internal.ObjectHandle handle, SignalDefinition signalDefinition, bool after, string? detail)
    {
        var detailQuark = GLib.Functions.QuarkFromString(detail);
        var handlerId = Internal.Functions.SignalConnectClosureById(handle.DangerousGetHandle(), signalDefinition.Id, detailQuark, Handle, after);

        if (handlerId.Value == 0)
            throw new Exception($"Could not connect to event {signalDefinition.ManagedName}");

        var key = (handle.DangerousGetHandle(), signalDefinition.Id);

        if (!_signalHandlers.ContainsKey(key))
            _signalHandlers[key] = [];

        _signalHandlers[key].Enqueue(handlerId);
        Debug.WriteLine($"Closure {Handle.DangerousGetHandle()}: Connected to {handle.DangerousGetHandle()} Signal '{signalDefinition.UnmanagedName}' with handlerId {handlerId.Value}");
    }

    public void Disconnect(Internal.ObjectHandle handle, SignalDefinition signalDefinition)
    {
        if (!_signalHandlers.TryGetValue((handle.DangerousGetHandle(), signalDefinition.Id), out var handlerIds))
        {
            Debug.Fail($"Closure {Handle.DangerousGetHandle()}: Could not disconnect from {handle.DangerousGetHandle()} Signal '{signalDefinition.UnmanagedName}' as no handlers got connected.");
            return;
        }

        if (!handlerIds.TryDequeue(out var handlerId))
        {
            Debug.Fail($"Closure {Handle.DangerousGetHandle()}: Could not disconnect from {handle.DangerousGetHandle()} Signal '{signalDefinition.UnmanagedName}' as no handlerId is left to disconnect.");
            return;
        }

        Internal.Functions.SignalHandlerDisconnect(handle.DangerousGetHandle(), handlerId);
        Debug.WriteLine($"Closure {Handle.DangerousGetHandle()} : Disconnected from {handle.DangerousGetHandle()} Signal '{signalDefinition.UnmanagedName}' with handlerId {handlerId.Value}");
    }
}
