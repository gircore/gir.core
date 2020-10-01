using System;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Collections.Generic;

namespace GObject
{
    public partial class Object
    {
        // See Object.TypeDictionary.cs for type mapping code
        // =====
        // This class purely contains an implementation for registering
        // user subclasses with GType.

        private static bool IsSubclass(System.Type type)
            => TypeDictionary.IsSubclass(type);

        // This returns the gtype for the furthest derived
        // wrapper class. It is the boundary between types defined in
        // GLib (wrappers) and types defined by the user (subclass)
        private static ulong GetBoundaryTypeId(System.Type type)
        {
            while (IsSubclass(type))
                type = type.BaseType!;
            
            return TypeDictionary.Get(type).Value;
        }

        // Query a gtype structure to find out information
        // like class size, etc, so we can allocate our own
        // type info struct deriving from it.
        private static TypeQuery QueryType(ulong gtype)
        {
            // Create query struct
            TypeQuery query = default;

            // Convert to Pointer
            var ptr = Marshal.AllocHGlobal(Marshal.SizeOf(query));
            Marshal.StructureToPtr(query, ptr, true);

            // Perform Query
            Sys.Methods.type_query(gtype, ptr);

            // Marshal and Free Memory
            query = (TypeQuery)Marshal.PtrToStructure(ptr, typeof(TypeQuery));
            Marshal.FreeHGlobal(ptr);

            return query;
        }
        
        // Class Initialiser
        private static void ClassInit(IntPtr g_class, IntPtr class_data)
        {
            Console.WriteLine("class_init: Initialising custom subclass!");
        }

        // TODO: Virtual Function
        private static void InstanceInit(IntPtr instance, IntPtr g_class)
        {
            Console.WriteLine("instance_init: Initialising custom subclass!");
        }

        private static string QualifyName(System.Type type)
            => $"{type.Namespace}_{type.Name}".Replace(".", "_");
        
        // Registers a new type class with the underlying GType type system
        private static void RegisterNativeType(System.Type type)
        {
            if (!IsSubclass(type))
                throw new Exception($"Error! Trying to register wrapper class {type} as new type");

            if (type.BaseType is {} && IsSubclass(type.BaseType))
                RegisterNativeType(type.BaseType);

            var boundaryTypeId = GetBoundaryTypeId(type);
            var query = QueryType(boundaryTypeId);

            // Create TypeInfo            
            var typeInfo = new TypeInfo(
                class_size: (ushort)query.class_size,
                instance_size: (ushort)query.instance_size,
                class_init: ClassInit,
                instance_init: InstanceInit
            );

            // Convert to Pointer
            var ptr = Marshal.AllocHGlobal(Marshal.SizeOf(typeInfo));
            Marshal.StructureToPtr(typeInfo, ptr, true);

            // Perform Registration
            var qualifiedName = QualifyName(type);
            Console.WriteLine($"Registering type {type.Name} as {qualifiedName}");
            var typeid = Sys.Methods.type_register_static(boundaryTypeId, qualifiedName, ptr, 0);

            if (typeid == 0)
                throw new Exception("Type Registration Failed!");

            // Free Memory
            Marshal.FreeHGlobal(ptr);

            // Register type in type dictionary
            TypeDictionary.Add(type, new Type(typeid));
        }

        private static Type TypeFromHandle(IntPtr handle)
        {
            try
            {
                var instance = Marshal.PtrToStructure<TypeInstance>(handle);
                var typeClass = Marshal.PtrToStructure<TypeClass>(instance.g_class);
                return new Type(typeClass.g_type);
            }
            catch
            {
                // TODO: Check if pointer is actually a GObject?
                throw new Exception("Could not resolve type from pointer");
            }
        }
    }
}