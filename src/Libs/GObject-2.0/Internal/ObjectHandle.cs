using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;

namespace GObject.Internal;

public class ObjectHandle : SafeHandle
{
    private readonly Dictionary<(uint, Delegate), (CULong, GObject.Closure)> _signalStore = new();

    public override bool IsInvalid => handle == IntPtr.Zero;

    public ObjectHandle(IntPtr handle, bool ownsHandle) : base(IntPtr.Zero, true)
    {
        SetHandle(handle);
        OwnReference(ownsHandle);
    }

    private void OwnReference(bool ownedRef)
    {
        if (!ownedRef)
        {
            // - Unowned GObjects need to be refed to bind them to this instance
            // - Unowned InitiallyUnowned floating objects need to be ref_sinked
            // - Unowned InitiallyUnowned non-floating objects need to be refed
            // As ref_sink behaves like ref in the case of non-floating instances, we use it for all 3 cases
            Object.RefSink(handle);
        }
        else
        {
            //In case we own the ref because the ownership was fully transferred to us, we
            //do not need to ref the object at all.

            Debug.Assert(!Object.IsFloating(handle), $"Handle {handle}: Owned floating references are not possible.");
        }
    }

    internal void Cache(GObject.Object obj)
    {
        Debug.Assert(handle == obj.Handle.DangerousGetHandle(), "Must cache the instance of this handle.");

        InstanceCache.Add(handle, obj);
    }

    internal void Connect(SignalDefinition signalDefinition, Delegate callback, GObject.Closure closure, bool after, string? detail)
    {
        var detailQuark = GLib.Functions.QuarkFromString(detail);
        var handlerId = Functions.SignalConnectClosureById(DangerousGetHandle(), signalDefinition.Id, detailQuark, closure.Handle, after);

        if (handlerId.Value == 0)
            throw new Exception($"Could not connect to event {signalDefinition.ManagedName}");

        _signalStore[(signalDefinition.Id, callback)] = (handlerId, closure);
    }

    internal void Disconnect(SignalDefinition signalDefinition, Delegate callback)
    {
        if (!_signalStore.TryGetValue((signalDefinition.Id, callback), out var tuple))
            return;

        Functions.SignalHandlerDisconnect(DangerousGetHandle(), tuple.Item1);
        tuple.Item2.Dispose();
        _signalStore.Remove((signalDefinition.Id, callback));
    }

    private void DisconnectSignals()
    {
        foreach (var item in _signalStore.Values)
        {
            Functions.SignalHandlerDisconnect(DangerousGetHandle(), item.Item1);
            item.Item2.Dispose();
        }

        _signalStore.Clear();
    }

    protected override bool ReleaseHandle()
    {
        RemoveMemoryPressure();
        DisconnectSignals();
        InstanceCache.Remove(handle);
        return true;
    }

    protected internal virtual void AddMemoryPressure() { }
    protected virtual void RemoveMemoryPressure() { }

    public static ObjectHandle For<T>(ConstructArgument[] constructArguments) where T : GObject.Object, GTypeProvider
    {
        // We can't check if a reference is floating via "g_object_is_floating" here
        // as the function could be "lying" depending on the intent of framework writers.
        // E.g. A Gtk.Window created via "g_object_new_with_properties" returns an unowned
        // reference which is not marked as floating as the gtk toolkit "owns" it.
        // For this reason we just delegate the problem to the caller and require a
        // definition whether the ownership of the new object will be transferred to us or not.

        var ptr = Object.NewWithProperties(
            objectType: T.GetGType(),
            nProperties: (uint) constructArguments.Length,
            names: constructArguments.Select(x => x.Name).ToArray(),
            values: GObject.Internal.ValueArray2OwnedHandle.Create(constructArguments.Select(x => x.Value).ToArray())
        );

        return new ObjectHandle(ptr, true);
    }
}
