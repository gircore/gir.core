using System;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.InteropServices;
using GLib;

namespace GObject.Native
{
    public class RecordWrapper
    {
        public static object WrapHandle(IntPtr handle, Type gtype)
        {
            System.Type trueType = TypeDictionary.GetSystemType(gtype);
            
            if (handle == IntPtr.Zero)
                throw new NullReferenceException($"Failed to wrap handle as type <{trueType}>. Null handle passed to WrapHandle.");

            // Get constructor for the true type
            ConstructorInfo? ctor = GetRecordConstructor(trueType);

            if (ctor == null)
                throw new Exception($"Type {trueType} does not define an IntPtr constructor. This could mean improperly defined bindings");

            return ctor.Invoke(new object[] { handle });
        }
        
        private static ConstructorInfo? GetRecordConstructor(System.Type type)
        {
            // Create using 'IntPtr' constructor
            ConstructorInfo? ctor = type.GetConstructor(
                System.Reflection.BindingFlags.NonPublic
                | System.Reflection.BindingFlags.Public
                | System.Reflection.BindingFlags.Instance,
                null, new[] { typeof(IntPtr) }, null
            );
            return ctor;
        }
    }
}
