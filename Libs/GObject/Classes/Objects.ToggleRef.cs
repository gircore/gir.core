using System;

namespace GObject
{
    public partial class Object
    {
        private class ToggleRef<T> where T : Object
        {
            private object _reference;

            public T? Object
            {
                get
                {
                    if (_reference is T obj)
                        return obj;

                    if (_reference is WeakReference<T> weakRef && weakRef.TryGetTarget(out T? refObj))
                        return refObj;

                    return null;
                }
            }

            /// <summary>
            /// Initializes a toggle ref. The given object must be already owned by C# as the owned
            /// reference is exchanged with a toggling reference.
            /// This object saves a strong reference to the given object which prevents it from beeing garbage
            /// collected. This strong reference is hold as long as there are other than our own toggling ref
            /// on the given object.
            /// If our toggeling ref is the lat ref on the given object the strong reference is changed into a
            /// weak reference. This allows the garbage collector to free the given C# object which will in turn
            /// free the last ref and thus free the unmanaged memory.
            /// </summary>
            public ToggleRef(T obj)
            {
                _reference = obj;
                Native.add_toggle_ref(obj.Handle, ToggleReference, IntPtr.Zero);
                Native.unref(obj.Handle);
            }

            private void ToggleReference(IntPtr data, IntPtr @object, bool is_last_ref)
            {
                if (is_last_ref && _reference is T obj)
                {
                    _reference = new WeakReference<T>(obj);
                }
                else if (!is_last_ref && _reference is WeakReference<T> weakRef)
                {
                    if (weakRef.TryGetTarget(out T? weakObj))
                        _reference = weakObj;
                    else
                        throw new Exception("Could not toggle reference to strong. It is garbage collected.");

                }
            }
        }
    }
}
