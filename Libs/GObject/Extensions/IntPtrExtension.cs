using System;
using System.Runtime.InteropServices;

namespace GObject
{
    internal static class IntPtrExtension
    {
        #region Methods
        
        public static Type GetGTypeFromTypeClass(this IntPtr typeClassPtr)
        {
            try
            {
                TypeClass typeClass = Marshal.PtrToStructure<TypeClass>(typeClassPtr);
                return new Type(typeClass.g_type);
            }
            catch(Exception ex)
            {
                // TODO: Check if pointer is actually a GObject?
                throw new Exception("Could not resolve type from pointer" + ex.Message);
            }
        }

        public static Type GetGTypeFromTypeInstance(this IntPtr typeInstancePtr)
        {
            try
            {
                TypeInstance typeInstance = Marshal.PtrToStructure<TypeInstance>(typeInstancePtr);
                return typeInstance.g_class.GetGTypeFromTypeClass();
            }
            catch
            {
                // TODO: Check if pointer is actually a GObject?
                throw new Exception("Could not resolve type from pointer");
            }
        }
        
        #endregion
    }
}
