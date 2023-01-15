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
        _handle = Internal.Closure.NewSimple((uint) Marshal.SizeOf<Internal.ClosureData>(), IntPtr.Zero);

        Debug.WriteLine($"Instantiating Closure: Address {_handle.DangerousGetHandle()}.");

        _closureMarshal = InternalCallback; //Save delegate to keep instance alive

        Internal.Closure.Ref(_handle);
        Internal.Closure.Sink(_handle);
        Internal.Closure.SetMarshal(_handle, _closureMarshal);
    }

    private void InternalCallback(IntPtr closure, IntPtr returnValuePtr, uint nParamValues, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)] Internal.ValueData[] paramValuesData, IntPtr invocationHint, IntPtr userData)
    {
        Debug.Assert(
            condition: paramValuesData.Length == nParamValues,
            message: "Values were not marshalled correctly. Breakage may occur"
        );

        var returnValue = returnValuePtr != IntPtr.Zero
            ? new Value(new Internal.ValueUnownedHandle(returnValuePtr))
            : null;

        var paramValues = paramValuesData
            .Select(valueData => Internal.ValueManagedHandle.Create(valueData))
            .Select(valueHandle => new Value(valueHandle))
            .ToArray();

        _callback(returnValue, paramValues);
    }

    public void Dispose()
    {
        Debug.WriteLine($"Disposing Closure: Address {_handle.DangerousGetHandle()}.");
        _handle.Dispose();
    }
}
