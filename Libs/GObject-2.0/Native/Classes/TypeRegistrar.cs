using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace GObject.Native
{
    internal class TypeRegistrationException : Exception
    {
        public TypeRegistrationException(string message) : base(message) { }
    }
    
    internal static class TypeRegistrar
    {
        // TODO: Split into two classes + abstract base class so we can
        // switch between reflection and source generation at compile time

        private static string QualifyName(System.Type type)
            => type.FullName?.Replace(".", "") ?? type.Name;
        
        private static TypeQuery.Struct GetTypeMetrics(Type parentType)
        {
            TypeQuery.Handle handle = TypeQuery.ManagedHandle.Create();
            Functions.TypeQuery(parentType.Value, handle);

            return Marshal.PtrToStructure<TypeQuery.Struct>(handle.DangerousGetHandle());
        }

        public static void RegisterSubclass(System.Type type)
        {
            Debug.Assert(
                condition: type.BaseType != null,
                message: "Cannot register a toplevel type - it must inherit from at least GObject"
            );
            
            System.Type baseType = type.BaseType;
            if (!TypeDictionary.ContainsSystemType(baseType))
                RegisterSubclass(baseType);
            
            RegisterType(type);
        }
        
        // Registers a new type class with the underlying GType type system
        private static void RegisterType(System.Type type)
        {
            Debug.Assert(
                condition: !TypeDictionary.ContainsSystemType(type),
                message: "The type dictionary should not contain the current type - we have not registered it yet"
            );
            
            Debug.Assert(
                condition: TypeDictionary.ContainsSystemType(type.BaseType!),
                message: "The type dictionary should contain the immediate parent type"
            );
            
            // Get metrics about parent type
            Type parentType = TypeDictionary.GetGType(type.BaseType!);
            TypeQuery.Struct query = GetTypeMetrics(parentType);

            if (query.Type == 0)
                throw new TypeRegistrationException("Could not query parent type");

            // Create TypeInfo
            var typeInfo = new TypeInfo.Struct()
            {
                ClassSize = (ushort) query.ClassSize,
                InstanceSize = (ushort) query.InstanceSize,
                ClassInit = DoClassInit,
                InstanceInit = DoInstanceInit
            };

            // Perform Registration
            var qualifiedName = QualifyName(type);
            Console.WriteLine($"Registering type {type.Name} as {qualifiedName}");

            TypeInfo.Handle handle = TypeInfo.ManagedHandle.Create(typeInfo);
            var typeid = Functions.TypeRegisterStatic(parentType.Value, qualifiedName, handle, 0);
            
            if (typeid == 0)
                throw new TypeRegistrationException("Type Registration Failed!");

            // Register type in type dictionary
            TypeDictionary.Add(type, new Type(typeid));
        }
        
        private static void DoClassInit(IntPtr gClass, IntPtr classData)
        {
            Console.WriteLine("Subclass type class initialised!");
        }
        
        private static void DoInstanceInit(IntPtr gClass, IntPtr classData)
        {
            Console.WriteLine("Subclass instance initialised!");
        }
    }
}
