using System;
using System.Reflection;
using GLib;

namespace GObject.Native
{
    public static class ObjectWrapper
    {
        public static T? WrapNullableHandle<T>(IntPtr handle, bool ownedRef) where T : class, IHandle
        {
            if (handle == IntPtr.Zero)
                return null;

            return WrapHandle<T>(handle, ownedRef);
        }
        
        public static T WrapHandle<T>(IntPtr handle, bool ownedRef) where T : class, IHandle
        {
            if (handle == IntPtr.Zero)
                throw new NullReferenceException($"Failed to wrap handle as type <{typeof(T).FullName}>. Null handle passed to WrapHandle.");

            if(ObjectMapper.TryGetObject(handle, out T? obj))
                return obj;

            // TODO: This is wrong - we need the constructor of the true type, not
            // type T (which is only the type we want to return as)
            // System.Type trueType = TypeDictionary.GetSystemType(handle);
            
            var ctor = GetObjectConstructor<T>();

            if (ctor == null)
                throw new Exception($"Type {typeof(T).FullName} does not define an IntPtr constructor. This could mean improperly defined bindings");

            return (T) ctor.Invoke(new object[] { handle, ownedRef });
        }

        private static ConstructorInfo? GetObjectConstructor<T>() where T : class
        {
            // Create using 'IntPtr' constructor
            ConstructorInfo? ctor = typeof(T).GetConstructor(
                System.Reflection.BindingFlags.NonPublic
                | System.Reflection.BindingFlags.Public
                | System.Reflection.BindingFlags.Instance,
                null, new[] { typeof(IntPtr), typeof(bool) }, null
            );
            return ctor;
        }
    }
}
