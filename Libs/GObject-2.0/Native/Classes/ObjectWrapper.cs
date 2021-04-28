using System;
using System.Reflection;
using System.Runtime.InteropServices;
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

            // Have we already mapped the object?
            if (ObjectMapper.TryGetObject(handle, out T? obj))
                return obj;

            // We need to find the "true type" of the object so that the user
            // can upcast/downcast as necessary. Retrieve the gtype from the
            // object's class struct.
            Type gtype = GetTypeFromHandle(handle);
            System.Type trueType = TypeDictionary.GetSystemType(gtype);
            
            // Get constructor for the true type
            ConstructorInfo? ctor = GetObjectConstructor(trueType);

            if (ctor == null)
                throw new Exception($"Type {typeof(T).FullName} does not define an IntPtr constructor. This could mean improperly defined bindings");

            return (T) ctor.Invoke(new object[] { handle, ownedRef });
        }

        private static Type GetTypeFromHandle(IntPtr handle)
        {
            TypeInstance.Struct instance = Marshal.PtrToStructure<TypeInstance.Struct>(handle);
            TypeClass.Struct classStruct = Marshal.PtrToStructure<TypeClass.Struct>(instance.GClass);
            return new Type(classStruct.GType);
        }

        private static ConstructorInfo? GetObjectConstructor(System.Type type)
        {
            // Create using 'IntPtr' constructor
            ConstructorInfo? ctor = type.GetConstructor(
                System.Reflection.BindingFlags.NonPublic
                | System.Reflection.BindingFlags.Public
                | System.Reflection.BindingFlags.Instance,
                null, new[] { typeof(IntPtr), typeof(bool) }, null
            );
            return ctor;
        }
    }
}
