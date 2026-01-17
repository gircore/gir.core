using System;
using System.Diagnostics;

namespace GObject.Internal;

internal class ToggleRef
{
    private object _reference;

    public ObjectHandle? Object
    {
        get
        {
            lock (InstanceCache.Lock)
            {
                if (_reference is WeakReference weakRef)
                    return (ObjectHandle?) weakRef.Target;

                return (ObjectHandle) _reference;
            }
        }
    }

    /// <summary>
    /// This object saves a strong reference to the given object handle which prevents it from being garbage
    /// collected. This strong reference is hold as long as there are other references than the toggling ref
    /// on the given object.
    /// If the toggeling ref is the last ref on the given object handle the strong reference is changed into a
    /// weak reference. This is signaled via a call to "ToggleReference".
    /// </summary>
    public ToggleRef(ObjectHandle obj)
    {
        _reference = obj;
    }

    internal void ToggleReference(bool isLastRef)
    {
        lock (InstanceCache.Lock)
        {
            if (!isLastRef && _reference is WeakReference weakRef)
            {
                if (weakRef.Target is { } weakObj)
                    _reference = weakObj;
                else
                    Console.WriteLine("Could not create a strong ref. The target got already garbage collected.");
            }
            else if (isLastRef && _reference is not WeakReference)
            {
                _reference = new WeakReference(_reference);
            }
        }
    }
}
