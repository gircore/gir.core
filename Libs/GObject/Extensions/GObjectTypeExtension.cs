using System;
using System.Runtime.InteropServices;

namespace GObject
{
    internal static class GObjectTypeExtension
    {
        #region Methods

        /// <summary>
        /// Query a gtype structure to find out information
        /// like class size, etc, so we can allocate our own
        /// type info struct deriving from it.
        /// </summary>
        public static TypeQuery QueryType(this Type type)
        {
            // Create query struct
            TypeQuery query = default;

            // Convert to Pointer
            IntPtr ptr = Marshal.AllocHGlobal(Marshal.SizeOf(query));
            Marshal.StructureToPtr(query, ptr, true);

            // Perform Query
            Global.Native.type_query(type.Value, ptr);

            // Marshal and Free Memory
            query = Marshal.PtrToStructure<TypeQuery>(ptr);
            Marshal.FreeHGlobal(ptr);

            return query;
        }
        #endregion
    }
}
