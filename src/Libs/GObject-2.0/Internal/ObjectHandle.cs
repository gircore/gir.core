using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace GObject.Internal;

public class ObjectHandle : SafeHandle
{
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
            // As ref_sink behaves like ref in case of non floating instances we use it for all 3 cases
            Object.RefSink(handle);
        }
        else
        {
            //In case we own the ref because the ownership was fully transfered to us we
            //do not need to ref the object at all.

            Debug.Assert(!Internal.Object.IsFloating(handle), $"Handle {handle}: Owned floating references are not possible.");
        }
    }

    internal void Cache(GObject.Object obj)
    {
        Debug.Assert(handle == obj.Handle.DangerousGetHandle(), "Must cache the instance of this handle.");

        InstanceCache.Add(handle, obj);
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
