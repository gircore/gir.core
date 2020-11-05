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
            query = (TypeQuery) Marshal.PtrToStructure(ptr, typeof(TypeQuery));
            Marshal.FreeHGlobal(ptr);

            return query;
        }
        #endregion
    }
}
