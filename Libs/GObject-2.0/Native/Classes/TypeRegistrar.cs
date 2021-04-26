using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace GObject.Native
{
    internal class TypeRegistrationException : Exception
    {
        public TypeRegistrationException(string message) : base(message) { }
    }

    public abstract class TypeRegistrar
    {
        private static TypeQuery.Struct GetTypeMetrics(Type parentType)
        {
            TypeQuery.Handle handle = TypeQuery.ManagedHandle.Create();
            Functions.TypeQuery(parentType.Value, handle);

            return Marshal.PtrToStructure<TypeQuery.Struct>(handle.DangerousGetHandle());
        }
        
        // Registers a new type class with the underlying GType type system
        protected void RegisterSubclass(System.Type type, System.Type parentType, string qualifiedName)
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
            Type parentGType = TypeDictionary.GetGType(parentType);
            TypeQuery.Struct query = GetTypeMetrics(parentGType);

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
            Console.WriteLine($"Registering type {type.Name} as {qualifiedName}");

            TypeInfo.Handle handle = TypeInfo.ManagedHandle.Create(typeInfo);
            var typeid = Functions.TypeRegisterStatic(parentGType.Value, qualifiedName, handle, 0);
            
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
