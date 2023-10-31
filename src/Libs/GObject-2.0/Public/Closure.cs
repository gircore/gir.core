using System;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;

namespace GObject;

public delegate void ClosureCallback(Value? returnValue, Value[] paramValues);

public partial class Closure : IDisposable
{
    private readonly ClosureCallback _callback;
    private readonly Internal.ClosureMarshal _closureMarshal; //Needed to keep delegate alive

    internal Closure(ClosureCallback callback)
    {
        _callback = callback;
        Handle = Internal.Closure.NewSimple((uint) Marshal.SizeOf<Internal.ClosureData>(), IntPtr.Zero);

        Debug.WriteLine($"Instantiating Closure: Address {Handle.DangerousGetHandle()}.");

        _closureMarshal = InternalCallback; //Save delegate to keep instance alive

        Internal.Closure.Ref(Handle);
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

    public void Dispose()
    {
        Debug.WriteLine($"Disposing Closure: Address {Handle.DangerousGetHandle()}.");
        Handle.Dispose();
    }
}
