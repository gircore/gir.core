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
            
            public ToggleRef(T obj)
            {
                _reference = obj;
                Native.add_toggle_ref(obj.Handle, ToggleReference, IntPtr.Zero);
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
