using System;
using System.Runtime.InteropServices;

namespace GObject
{
    public partial class Object
    {
        // Given a type, it will walk up the object's inheritance tree
        // to find the first non-user-defined class (i.e. implemented by
        // a C library).
        private static Type GetBoundaryType(Type type)
        {
            while (IsSubclass(type))
                type = type.BaseType;
            
            return type;
        }

        private static Sys.TypeQuery QueryType(ulong gtype)
        {
            // Create query struct
            Sys.TypeQuery query = default;

            // Convert to Pointer
            IntPtr ptr = Marshal.AllocHGlobal(Marshal.SizeOf(query));
            Marshal.StructureToPtr(query, ptr, true);

            // Perform Query
            Sys.Methods.type_query((ulong)gtype, ptr);

            // Marshal and Free Memory
            query = (Sys.TypeQuery)Marshal.PtrToStructure(ptr, typeof(Sys.TypeQuery));
            Marshal.FreeHGlobal(ptr);
            ptr = IntPtr.Zero;

            return query;
        }

        // Class Initialiser
        private static void ClassInit(IntPtr g_class, IntPtr class_data)
        {
            Console.WriteLine("class_init: Initialising custom subclass!");
        }

        // Assumes the class has not been registered
        private static void RegisterClass(Type type)
        {
            // Check the class has not been registered already
            if (TypeDictionary.Contains(type))
                return;

            // Call recursively to register the hierarchy
            var basetype = type.BaseType;
            if (IsSubclass(basetype) && !TypeDictionary.Contains(basetype))
                RegisterClass(basetype);

            // Get Boundary Type
            var boundary = GetBoundaryType(type);
            ulong gtype = TypeDictionary.FromType(boundary);
            Sys.TypeQuery query = QueryType(gtype);

            // Get Immediate Parent Type
            ulong parentType = TypeDictionary.FromType(basetype);

            // TODO: Use reflection to lookup properties, virtual function overrides, etc
            
            // Create TypeInfo            
            var typeInfo = new Sys.TypeInfo(
                class_size: (ushort)query.class_size,
                instance_size: (ushort)query.instance_size,
                class_init: ClassInit
            );

            // Convert to Pointer
            IntPtr ptr = Marshal.AllocHGlobal(Marshal.SizeOf(typeInfo));
            Marshal.StructureToPtr(typeInfo, ptr, true);

            // Perform Registration
            string qualifiedName = (type.Namespace + type.Name).Replace(".", "");
            Console.WriteLine($"Registering type {type.Name} as {qualifiedName}");
            ulong typeid = Sys.Methods.type_register_static(parentType, qualifiedName, ptr, 0);

            if (typeid == 0)
                throw new Exception("Type Registration Failed!");

            // Free Memory
            Marshal.FreeHGlobal(ptr);
            ptr = IntPtr.Zero;

            // Register in Type Dictionary
            TypeDictionary.RegisterType(type, typeid);
        }
    }
}