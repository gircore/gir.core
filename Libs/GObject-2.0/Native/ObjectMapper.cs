using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace GObject.Native
{
    public static partial class ObjectMapper
    {
        private static readonly Dictionary<IntPtr, ToggleRef<object>> WrapperObjects = new();
        
        public static bool TryGetObject<T>(IntPtr handle, [NotNullWhen(true)] out T? obj) where T : class
        {
            if (WrapperObjects.TryGetValue(handle, out ToggleRef<object>? weakRef))
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

        public static void Map(IntPtr handle, object obj)
        {
            WrapperObjects[handle] = new ToggleRef<object>(obj);
        }

        public static void Unmap(IntPtr handle)
        {
            WrapperObjects.Remove(handle);
        }
    }
}
