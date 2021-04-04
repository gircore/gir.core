using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using GLib;

namespace GObject.Native
{
    public static partial class ObjectMapper
    {
        private static readonly Dictionary<IntPtr, ToggleRef<IHandle>> WrapperObjects = new();
        
        public static bool TryGetObject<T>(IntPtr handle, [NotNullWhen(true)] out T? obj) where T : class, IHandle
        {
            if (WrapperObjects.TryGetValue(handle, out ToggleRef<IHandle>? weakRef))
            {
                if (weakRef.Object is not null)
                {
                    obj = (T) weakRef.Object;
                    return true;
                }
            }

            obj = null;
            return false;
        }

        public static void Map(IntPtr handle, IHandle obj)
        {
            WrapperObjects[handle] = new ToggleRef<IHandle>(obj);
        }

        public static void Unmap(IntPtr handle)
        {
            WrapperObjects.Remove(handle);
        }
    }
}
