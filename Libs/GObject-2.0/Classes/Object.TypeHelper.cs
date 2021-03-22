using System;
using System.Reflection;
using System.Runtime.InteropServices;

namespace GObject
{
    public partial class Object
    {
        protected static class TypeHelper
        {
            #region Methods

            public static IntPtr GetClassPointer(Type type)
            {
                IntPtr ptr = TypeClass.Native.peek(type.Value);

                if (ptr == IntPtr.Zero)
                    ptr = TypeClass.Native.@ref(type.Value);

                return ptr;
            }

            internal static ClassInitFuncCallback GetClassInitFunc(IReflect type) => (gClass, classData) =>
            {
                Type gtype = GetGTypeFromTypeClass(gClass);

                InvokeStaticMethod(
                    type: type,
                    name: "ClassInit",
                    parameters: new object[] { gtype, type, classData }
                );
            };

            private static void InvokeStaticMethod(IReflect type, string name, params object[] parameters)
            {
                MethodInfo? method = type.GetMethod(
                    name: name,
                    bindingAttr:
                    System.Reflection.BindingFlags.Static
                    | System.Reflection.BindingFlags.DeclaredOnly
                    | System.Reflection.BindingFlags.NonPublic
                );

                method?.Invoke(null, parameters);
            }

            private static Type GetGTypeFromTypeClass(IntPtr typeClassPtr)
            {
                try
                {
                    TypeClass typeClass = Marshal.PtrToStructure<TypeClass>(typeClassPtr);
                    return new Type(typeClass.g_type);
                }
                catch (Exception ex)
                {
                    // TODO: Check if pointer is actually a GObject?
                    throw new Exception("Could not resolve type from pointer" + ex.Message);
                }
            }

            #endregion
        }
    }
}
