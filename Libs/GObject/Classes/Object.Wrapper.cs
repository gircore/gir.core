using System;
using System.Diagnostics.CodeAnalysis;

namespace GObject
{
    public partial class Object
    {
        public class Wrapper
        {
            public static T? WrapNullableHandle<T>(IntPtr handle, bool ownedRef) where T : Object
            {
                if (handle == IntPtr.Zero)
                    return null;

                return WrapHandle<T>(handle, ownedRef);
            }
            
            /// <summary>
            /// A variant of <see cref="WrapHandle{T}"/> which fails gracefully if the pointer cannot be wrapped.
            /// </summary>
            /// <param name="handle">A pointer to the native GObject that should be wrapped.</param>
            /// <param name="o">A C# proxy object which wraps the native GObject.</param>
            /// <param name="ownedRef">Specify if the ref is owned by us, because ownership was transferred.</param>
            /// <typeparam name="T"></typeparam>
            /// <returns><c>true</c> if the handle was wrapped, or <c>false</c> if something went wrong.</returns>
            public static bool TryWrapHandle<T>(IntPtr handle, bool ownedRef, [NotNullWhen(true)] out T? o)
                where T : Object
            {
                o = null;
                try
                {
                    o = WrapHandle<T>(handle, ownedRef);
                    return true;
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Could not wrap handle as type {typeof(T).FullName}: {e.Message}");
                    return false;
                }
            }

            /// <summary>
            /// This function returns the proxy object to the provided handle
            /// if it already exists, otherwise creates a new wrapper object
            /// and returns it. Note that <typeparamref name="T"/> is the type
            /// the object should be returned. It is independent of the object's
            /// actual type and is provided purely for convenience.
            /// </summary>
            /// <param name="handle">A pointer to the native GObject that should be wrapped.</param>
            /// <param name="ownedRef">Specify if the ref is owned by us, because ownership was transferred.</param>
            /// <typeparam name="T"></typeparam>
            /// <returns>A C# proxy object which wraps the native GObject.</returns>
            /// <exception cref="NullReferenceException"></exception>
            /// <exception cref="InvalidCastException"></exception>
            /// <exception cref="Exception"></exception>
            public static T WrapHandle<T>(IntPtr handle, bool ownedRef) where T : Object
            {
                if (handle == IntPtr.Zero)
                    throw new NullReferenceException(
                        $"Failed to wrap handle as type <{typeof(T).FullName}>. Null handle passed to WrapHandle.");

                if (ReferenceManager.TryGetObject(handle, out T? obj))
                    return obj;

                // Resolve GType of object
                Type trueGType = TypeFromHandle(handle);
                System.Type? trueType = null;

                // Ensure 'T' is registered in type dictionary for future use. It is an error for a
                // wrapper type to not define a TypeDescriptor. 
                TypeDescriptor desc = TypeDescriptorRegistry.ResolveTypeDescriptorForType(typeof(T));

                TypeDictionary.AddRecursive(typeof(T), desc.GType);

                // Optimisation: Compare the gtype of 'T' to the GType of the pointer. If they are
                // equal, we can skip the type dictionary's (possible) recursive lookup and return
                // immediately.
                if (desc.GType.Equals(trueGType))
                {
                    // We are actually a type 'T'.
                    // The conversion will always be valid
                    trueType = typeof(T);
                }
                else
                {
                    // We are some other representation of 'T' (e.g. a more derived type)
                    // Retrieve the normal way
                    trueType = TypeDictionary.Get(trueGType);

                    // Ensure the conversion is valid
                    Type castGType = TypeDictionary.Get(typeof(T));
                    if (!Global.Native.type_is_a(trueGType.Value, castGType.Value))
                        throw new InvalidCastException();
                }

                // Create using 'IntPtr' constructor
                System.Reflection.ConstructorInfo? ctor = trueType.GetConstructor(
                    System.Reflection.BindingFlags.NonPublic
                    | System.Reflection.BindingFlags.Public
                    | System.Reflection.BindingFlags.Instance,
                    null, new[] {typeof(IntPtr), typeof(bool)}, null
                );

                if (ctor == null)
                    throw new Exception(
                        $"Type {trueType.FullName} does not define an IntPtr constructor. This could mean improperly defined bindings");

                return (T) ctor.Invoke(new object[] {handle, ownedRef});
            }
        }
    }
}
