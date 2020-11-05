using System;
using System.Runtime.InteropServices;

namespace GObject
{
    internal static class IntPtrExtension
    {
        #region Methods

        public static Type GetGType(this IntPtr handle)
        {
            try
            {
                TypeInstance instance = Marshal.PtrToStructure<TypeInstance>(handle);
                TypeClass typeClass = Marshal.PtrToStructure<TypeClass>(instance.g_class);
                return new Type(typeClass.g_type);
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
