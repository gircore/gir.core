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

            public static TypeClass.Native.TypeClassSafeHandle GetClassPointer(Type type)
            {
                var ptr = TypeClass.Native.Methods.Peek(type.Value);

                if (ptr.IsInvalid)
                    ptr = TypeClass.Native.Methods.Ref(type.Value);

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

            private static Type GetGTypeFromTypeClass(TypeClass.Native.TypeClassSafeHandle typeClassPtr)
            {
                try
                {
                    var typeClass = Marshal.PtrToStructure<TypeClass.Native.Struct>(typeClassPtr.DangerousGetHandle());
                    return new Type(typeClass.GType);
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
