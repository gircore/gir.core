using System;

namespace GObject
{
    public partial class Object
    {
        protected static class Wrapper
        {
            public static T WrapHandle<T>(IntPtr handle, bool ownedRef) where T : class
            {
                if (handle == IntPtr.Zero)
                    throw new NullReferenceException($"Failed to wrap handle as type <{typeof(T).FullName}>. Null handle passed to WrapHandle.");

                // Create using 'IntPtr' constructor
                System.Reflection.ConstructorInfo? ctor = typeof(T).GetConstructor(
                    System.Reflection.BindingFlags.NonPublic
                    | System.Reflection.BindingFlags.Public
                    | System.Reflection.BindingFlags.Instance,
                    null, new[] { typeof(IntPtr), typeof(bool) }, null
                );

                if (ctor == null)
                    throw new Exception($"Type {typeof(T).FullName} does not define an IntPtr constructor. This could mean improperly defined bindings");

                return (T) ctor.Invoke(new object[] { handle, ownedRef });
            }
        }
    }
}
