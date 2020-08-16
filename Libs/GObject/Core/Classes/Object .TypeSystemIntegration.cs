using System;
using System.Reflection;
using System.Runtime.InteropServices;

namespace GObject
{
    public partial class Object
    {
        private static ulong GetBoundaryTypeId(Type type)
        {
            while (IsSubclass(type))
                type = type.BaseType!;
            
            return GetGTypeFor(type);
        }

        private static bool IsSubclass(Type type)
            => type != typeof(Object) &&
               type != typeof(InitallyUnowned) &&
               GetGTypeMethodInfo(type) is null;

        private static Sys.TypeQuery QueryType(ulong gtype)
        {
            // Create query struct
            Sys.TypeQuery query = default;

            // Convert to Pointer
            var ptr = Marshal.AllocHGlobal(Marshal.SizeOf(query));
            Marshal.StructureToPtr(query, ptr, true);

            // Perform Query
            Sys.Methods.type_query(gtype, ptr);

            // Marshal and Free Memory
            query = (Sys.TypeQuery)Marshal.PtrToStructure(ptr, typeof(Sys.TypeQuery));
            Marshal.FreeHGlobal(ptr);

            return query;
        }
        
        // Class Initialiser
        private static void ClassInit(IntPtr g_class, IntPtr class_data)
        {
            Console.WriteLine("class_init: Initialising custom subclass!");
        }

        private static void InstanceInit(IntPtr instance, IntPtr g_class)
        {
            Console.WriteLine("instance_init: Initialising custom subclass!");
        }

        private static string GetQualifiedName(Type type)
            => $"{type.Namespace}_{type.Name}".Replace(".", "_");

        private static MethodInfo? GetGTypeMethodInfo(Type type)
        {
            const BindingFlags flags = BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.DeclaredOnly;
            return type.GetMethod(nameof(GetGType), flags);
        }
        
        private static Sys.Type GetGTypeFor(Type type)
        {
            if (IsSubclass(type))
            {
                var qualifiedName = GetQualifiedName(type);
                return new Sys.Type(Sys.Methods.type_from_name(qualifiedName));
            }

            var getTypeIdMethod = GetGTypeMethodInfo(type);
            if (getTypeIdMethod is {})
                return (Sys.Type) getTypeIdMethod.Invoke(null, null); //This ensures the type registration

            return Sys.Type.Invalid;
        }
        
        private static void RegisterType(Type type)
        {
            if (GetGTypeFor(type) != Sys.Type.Invalid)
                return; //Type is already registered
            
            if(type.BaseType is {} && IsSubclass(type.BaseType))
                RegisterType(type.BaseType);

            var boundaryTypeId = GetBoundaryTypeId(type);
            var query = QueryType(boundaryTypeId);

            // Create TypeInfo            
            var typeInfo = new Sys.TypeInfo(
                class_size: (ushort)query.class_size,
                instance_size: (ushort)query.instance_size,
                class_init: ClassInit,
                instance_init: InstanceInit
            );

            // Convert to Pointer
            var ptr = Marshal.AllocHGlobal(Marshal.SizeOf(typeInfo));
            Marshal.StructureToPtr(typeInfo, ptr, true);

            // Perform Registration
            var qualifiedName = GetQualifiedName(type);
            Console.WriteLine($"Registering type {type.Name} as {qualifiedName}");
            var typeid = Sys.Methods.type_register_static(boundaryTypeId, qualifiedName, ptr, 0);

            if (typeid == 0)
                throw new Exception("Type Registration Failed!");

            // Free Memory
            Marshal.FreeHGlobal(ptr);
        }
    }
}