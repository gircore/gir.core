using System;
using System.Diagnostics;

namespace GObject.Internal;

public partial class ObjectMapper
{
    private class ToggleRef : IDisposable
    {
        private object _reference;
        private readonly ToggleNotify _callback;
        private readonly IntPtr _handle;

        public object? Object
        {
            get
            {
                if (_reference is not WeakReference weakRef)
                    return _reference;

                if (weakRef.Target is { } target)
                    return target;

                return null;
            }
        }

        /// <summary>
        /// Initializes a toggle ref. The given object must be already owned by C# as the owned
        /// reference is exchanged with a toggling reference meaning the toggle reference is taking control
        /// over the reference.
        /// This object saves a strong reference to the given object which prevents it from beeing garbage
        /// collected. This strong reference is hold as long as there are other than our own toggling ref
        /// on the given object.
        /// If our toggeling ref is the last ref on the given object the strong reference is changed into a
        /// weak reference. This allows the garbage collector to free the C# object which must result in the
        /// call of the Dispose method of the ToggleRef. The Dispose mehtod removes the added toggle reference
        /// and thus frees the last reference to the C object.
        /// </summary>
        public ToggleRef(IntPtr handle, object obj, bool ownedRef)
        {
            _reference = obj;
            _callback = ToggleReference;
            _handle = handle;

            OwnReference(ownedRef);
            RegisterToggleRef();
        }

        private void RegisterToggleRef()
        {
            Internal.Object.AddToggleRef(_handle, _callback, IntPtr.Zero);
            Internal.Object.Unref(_handle);
        }

        private void OwnReference(bool ownedRef)
        {
            if (!ownedRef)
            {
                // - Unowned GObjects need to be refed to bind them to this instance
                // - Unowned InitiallyUnowned floating objects need to be ref_sinked
                // - Unowned InitiallyUnowned non-floating objects need to be refed
                // As ref_sink behaves like ref in case of non floating instances we use it for all 3 cases
                Internal.Object.RefSink(_handle);
            }
            else
            {
                //In case we own the ref because the ownership was fully transfered to us we
                //do not need to ref the object at all.

                Debug.Assert(!Internal.Object.IsFloating(_handle), "Owned floating references are not possible.");
            }
        }

        private void ToggleReference(IntPtr data, IntPtr @object, bool isLastRef)
        {
            if (!isLastRef && _reference is WeakReference weakRef)
            {
                if (weakRef.Target is { } weakObj)
                    _reference = weakObj;
                else
                    throw new Exception("Could not toggle reference to strong. It is garbage collected.");
            }
            else if (isLastRef && _reference is not WeakReference)
            {
                _reference = new WeakReference(_reference);
            }
        }

        public void Dispose()
        {
            Internal.Object.RemoveToggleRef(_handle, _callback, IntPtr.Zero);
        }
    }
}
