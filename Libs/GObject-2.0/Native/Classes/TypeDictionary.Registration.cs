using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace GObject.Native
{
    public static partial class TypeDictionary
    {
        // TODO: This stuff needs to be hidden behind a ITypeInitialiser interface
        // so we can switch between reflection and source generation at compile time

        private class TypeRegistrationException : Exception
        {
            public TypeRegistrationException(string message) : base(message) { }
        }

        private static string QualifyName(System.Type type)
            => type.FullName?.Replace(".", "") ?? type.Name;
        
        private static TypeQuery.Struct GetTypeMetrics(Type parentType)
        {
            TypeQuery.Handle handle = TypeQuery.ManagedHandle.Create();
            Functions.TypeQuery(parentType.Value, handle);

            return Marshal.PtrToStructure<TypeQuery.Struct>(handle.DangerousGetHandle());
        }

        private static void RegisterSubclassRecursive(System.Type type)
        {
            Debug.Assert(
                condition: type.BaseType != null,
                message: "Cannot register a toplevel type - it must inherit from at least GObject"
            );
            
            System.Type baseType = type.BaseType;
            if (!_systemTypeDict.ContainsKey(baseType))
                RegisterSubclassRecursive(baseType);
            
            RegisterSubclass(type);
        }
        
        // Registers a new type class with the underlying GType type system
        private static void RegisterSubclass(System.Type type)
        {
            Debug.Assert(
                condition: !_systemTypeDict.ContainsKey(type),
                message: "The type dictionary should not contain the current type - we have not registered it yet"
            );
            
            Debug.Assert(
                condition: _systemTypeDict.ContainsKey(type.BaseType!),
                message: "The type dictionary should contain the immediate parent type"
            );
            
            // Get metrics about parent type
            Type parentType = _systemTypeDict[type.BaseType];
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
            Add(type, new Type(typeid));
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
