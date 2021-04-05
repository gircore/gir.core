﻿using System;
using GLib;

namespace GObject.Native
{
    public partial class ObjectMapper
    {
        private class ToggleRef<T> where T : class, IHandle
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

                Native.Object.Instance.Methods.AddToggleRef(obj.Handle, ToggleReference, IntPtr.Zero);
                Native.Object.Instance.Methods.Unref(obj.Handle);
            }

            private void ToggleReference(IntPtr data, IntPtr @object, bool isLastRef)
            {
                if (isLastRef && _reference is T obj)
                {
                    _reference = new WeakReference<T>(obj);
                }
                else if (!isLastRef && _reference is WeakReference<T> weakRef)
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