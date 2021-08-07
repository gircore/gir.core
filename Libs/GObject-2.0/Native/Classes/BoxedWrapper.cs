using System;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.InteropServices;
using GLib;

namespace GObject.Native
{
    public class BoxedWrapper
    {
        public static object WrapHandle(IntPtr handle, Type gtype)
        {
            System.Type trueType = TypeDictionary.GetSystemType(gtype);
            
            if (handle == IntPtr.Zero)
                throw new NullReferenceException($"Failed to wrap handle as type <{trueType}>. Null handle passed to WrapHandle.");

            // Get constructor for the true type
            var ctr = GetBoxedConstructor(trueType);

            object? result;
            
            if (ctr == null)
            {
                //If we do not find an constructor we try to find our secret factory method.
                //TODO: This is a workaround for Gdk.Event Should get obsolete with GTK4
                var methodInfo = GetSecretFactoryMethod(trueType);
                
                if(methodInfo is null)
                    throw new Exception($"Type {trueType} does not define an IntPtr constructor. This could mean improperly defined bindings");
                
                result = methodInfo.Invoke(null, new object[] { handle });
            }
            else
            {
                result = ctr.Invoke(new object[] { handle });   
            }

            if (result == null)
                throw new Exception($"Type {trueType}'s factory method returned a null object. This could mean improperly defined bindings");

            return result;
        }
        
        private static MethodInfo? GetSecretFactoryMethod(System.Type type)
        {
            // Create using 'IntPtr' constructor
            MethodInfo? ctor = type.GetMethod("__FactoryNew",
                System.Reflection.BindingFlags.NonPublic
                | System.Reflection.BindingFlags.Static,
                null, new[] { typeof(IntPtr) }, null
            );
            return ctor;
        }
        
        private static ConstructorInfo? GetBoxedConstructor(System.Type type)
        {
            // Create using 'IntPtr' constructor
            ConstructorInfo? ctor = type.GetConstructor(
                System.Reflection.BindingFlags.NonPublic
                | System.Reflection.BindingFlags.Public
                | System.Reflection.BindingFlags.Instance,
                null, new[] { typeof(IntPtr), }, null
            );
            return ctor;
        }
    }
}
