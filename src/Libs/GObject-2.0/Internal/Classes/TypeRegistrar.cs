using System;
using System.Runtime.InteropServices;

namespace GObject.Internal
{
    /// <summary>
    /// Thrown when type registration with GType fails
    /// </summary>
    internal class TypeRegistrationException : Exception
    {
        public TypeRegistrationException(string message) : base(message) { }
    }

    /// <summary>
    /// A set of utility functions to register new types with the
    /// GType dynamic type system.
    /// </summary>
    public static class TypeRegistrar
    {
        private static TypeQueryData GetTypeMetrics(Type parentType)
        {
            TypeQuery.Handle handle = TypeQuery.ManagedHandle.Create();
            Functions.TypeQuery(parentType.Value, handle);

            return Marshal.PtrToStructure<TypeQueryData>(handle.DangerousGetHandle());
        }

        /// <summary>
        /// Registers with GType a new child class of 'parentType'.
        /// </summary>
        /// <param name="qualifiedName">The name of the class</param>
        /// <param name="parentType">The parent class to derive from</param>
        /// <returns>The newly registered type</returns>
        /// <exception cref="TypeRegistrationException">The type could not be registered</exception>
        internal static Type RegisterGType(string qualifiedName, Type parentType)
        {
            // Get metrics about parent type
            TypeQueryData query = GetTypeMetrics(parentType);

            if (query.Type == 0)
                throw new TypeRegistrationException("Could not query parent type");

            // Create TypeInfo
            var typeInfo = new TypeInfoData()
            {
                ClassSize = (ushort) query.ClassSize,
                InstanceSize = (ushort) query.InstanceSize,
                ClassInit = DoClassInit,
                InstanceInit = DoInstanceInit
            };

            // Perform Registration
            Console.WriteLine($"Registering new type {qualifiedName} with parent {parentType.ToString()}");

            TypeInfo.Handle handle = TypeInfo.ManagedHandle.Create(typeInfo);
            var typeid = Functions.TypeRegisterStatic(parentType.Value, qualifiedName, handle, 0);

            if (typeid == 0)
                throw new TypeRegistrationException("Type Registration Failed!");

            return new Type(typeid);
        }

        // Default Handler for class initialisation.
        private static void DoClassInit(IntPtr gClass, IntPtr classData)
        {
            Console.WriteLine("Subclass type class initialised!");
        }

        // Default Handler for instance initialisation.
        private static void DoInstanceInit(IntPtr gClass, IntPtr classData)
        {
            Console.WriteLine("Subclass instance initialised!");
        }
    }
}
