using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace GObject
{
    public partial class Object
    {
        private static class ReferenceManager
        {
            #region Fields
            
            private static readonly Dictionary<IntPtr, ToggleRef<Object>> SubclassObjects = new ();
            private static readonly Dictionary<IntPtr, WeakReference<Object>> WrapperObjects = new ();
            
            #endregion
            
            #region Methods
            
            public static void RegisterObject(Object obj)
            {
                if(IsSubclass(obj.GetType()))
                    SubclassObjects.Add(obj.Handle, new ToggleRef<Object>(obj));
                else
                    WrapperObjects.Add(obj.Handle, new WeakReference<Object>(obj));
            }

            public static void RemoveObject(Object obj)
            {
                WrapperObjects.Remove(obj.Handle);
                SubclassObjects.Remove(obj.Handle);
            }
            
            public static bool TryGetObject<T>(IntPtr handle, [NotNullWhen(true)] out T? obj) where T : Object
            {
                if (WrapperObjects.TryGetValue(handle, out WeakReference<Object>? weakRef))
                {
                    if (weakRef.TryGetTarget(out Object? weakObj))
                    {
                        obj = (T) weakObj;
                        return true;
                    }

                    WrapperObjects.Remove(handle);
                }
                else if (SubclassObjects.TryGetValue(handle, out ToggleRef<Object>? toggleObj))
                {
                    if (toggleObj.Object is not null)
                    {
                        obj = (T) toggleObj.Object;
                        return true;
                    }

                    SubclassObjects.Remove(handle);
                }

                obj = null;
                return false;
            }
            
            #endregion
        }
    }
}
