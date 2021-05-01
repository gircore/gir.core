using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using GLib;

namespace GObject.Native
{
    public static partial class ObjectMapper
    {
        private static readonly Dictionary<IntPtr, ToggleRef> WrapperObjects = new();

        public static bool TryGetObject<T>(IntPtr handle, [NotNullWhen(true)] out T? obj) where T : class, IHandle
        {
            if (WrapperObjects.TryGetValue(handle, out ToggleRef? weakRef))
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

        public static void Map(IntPtr handle, object obj, bool ownedRef)
        {
            lock (WrapperObjects)
            {
                WrapperObjects[handle] = new ToggleRef(handle, obj, ownedRef);
            }
        }

        public static void Unmap(IntPtr handle)
        {
            lock (WrapperObjects)
            {
                if (WrapperObjects.Remove(handle, out var toggleRef))
                    toggleRef.Dispose();
            }
        }
    }
}
