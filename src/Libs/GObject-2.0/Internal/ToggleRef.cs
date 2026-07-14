using System;

namespace GObject.Internal;

internal class ToggleRef : IDisposable
{
    private object _reference;
    private bool _disposed;

    public GObject.Object? Object
    {
        get
        {
            if (!_disposed)
                return null;

            if (_reference is WeakReference weakRef)
                return (GObject.Object?) weakRef.Target;

            return (GObject.Object) _reference;
        }
    }

    /// <summary>
    /// This object saves a strong reference to the given object which prevents it from beeing garbage
    /// collected. This strong reference is hold as long as there are other references than the toggling ref
    /// on the given object.
    /// If the toggeling ref is the last ref on the given object the strong reference is changed into a
    /// weak reference. This is signaled via a call to "ToggleReference".
    /// </summary>
    public ToggleRef(GObject.Object obj)
    {
        _reference = obj;
    }

    internal void ToggleReference(bool isLastRef)
    {
        if (_disposed)
            return;
        
        if (!isLastRef && _reference is WeakReference weakRef)
        {
            if (weakRef.Target is { } weakObj)
                _reference = weakObj;
            else
                throw new Exception("Could not toggle reference to strong. It got garbage collected. There is an obsolete dotnet instance still used by native code. Find it and actively call 'Dispose' on it or avoid it getting obsolete. Use the 'Handle' property to verify the pointer to a native instance.");
        }
        else if (isLastRef && _reference is not WeakReference)
        {
            _reference = new WeakReference(_reference);
        }
    }

    public void Dispose()
    {
        _disposed = true;
    }
}
