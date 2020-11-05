using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;

namespace GObject
{
    internal static class SystemTypeExtension
    {
        #region Fields
        private static readonly Dictionary<System.Type, Object.TypeDescriptor?> DescriptorDict = new Dictionary<System.Type, Object.TypeDescriptor?>();

        #endregion
        
        #region Methods
        
        private static string QualifyName(this System.Type type)
            => $"{type.Namespace}_{type.Name}".Replace(".", "_");
        
        /// <summary>
        /// Determines whether the type is a managed subclass,
        /// as opposed to wrapping an existing type.
        /// </summary>
        public static bool IsSubclass(this System.Type type)
            => type != typeof(Object) &&
               type != typeof(InitiallyUnowned) &&
               type.GetTypeDescriptor() is null;


        public static Type Register(this System.Type type)
        {
            //TODO: Register recursively to save CPU cylcles instead
            //to register only the required types?
            
            if (Object.TypeDictionary.TryGet(type, out Type gtype))
                return gtype;
            
            Object.TypeDescriptor? typeDescriptor = type.GetTypeDescriptor();

            if (typeDescriptor is { })
            {
                //Type is a native class
                Object.TypeDictionary.Register(type, typeDescriptor.GType);
                return typeDescriptor.GType;
            }
            else
            {
                //Type is a subclass, because custom type does not have a type descriptor
                //We create a new type for this. As a base we use the first
                //native class we find in its hierarchy.

                System.Type boundarySystemType = type.GetBoundaryType();
                Type boundaryGtype = boundarySystemType.Register();//Register in case this is not registred
                TypeQuery query = boundaryGtype.QueryType();
                
                // Create TypeInfo
                var typeInfo = new TypeInfo(
                    class_size: (ushort) query.class_size,
                    instance_size: (ushort) query.instance_size,
                    class_init: type.GetClassInitFunc(),
                    instance_init: type.GetInstanceInitFunc()
                );
                
                // Convert to Pointer
                IntPtr typeInfoPtr = Marshal.AllocHGlobal(Marshal.SizeOf(typeInfo));
                Marshal.StructureToPtr(typeInfo, typeInfoPtr, true);

                // Perform Registration
                var qualifiedName = type.QualifyName();
                Console.WriteLine($"Registering type {type.Name} as {qualifiedName}");
                var typeid = Global.Native.type_register_static(boundaryGtype.Value, qualifiedName, typeInfoPtr, 0);

                if (typeid == 0)
                    throw new Exception("Type Registration Failed!");

                // Free Memory
                Marshal.FreeHGlobal(typeInfoPtr);

                var subclassType = new Type(typeid);
                
                // Register type in type dictionary
                Object.TypeDictionary.Register(type, subclassType);
                return subclassType;
            }
        }
        
        /// <summary>
        /// This returns the System.Type for the furthest derived
        /// wrapper class. It is the boundary between types defined in
        /// GLib (wrappers) and types defined by the user (subclass)
        /// </summary>
        private static System.Type GetBoundaryType(this System.Type type)
        {
            while (IsSubclass(type))
                type = type.BaseType!;

            return type;
        }
        
        private static ClassInitFunc? GetClassInitFunc(this System.Type type) => (gClass, classData) =>
        {
            MethodInfo? method = type.GetMethod(
                name: "ClassInit",
                bindingAttr:
                System.Reflection.BindingFlags.Static
                | System.Reflection.BindingFlags.DeclaredOnly
                | System.Reflection.BindingFlags.NonPublic
            );

            if (method is null)
                return;

            method.Invoke(null, new object[] {gClass, classData});
        };

        private static InstanceInitFunc GetInstanceInitFunc(this System.Type type) => (instance, gClass) =>
        {
            MethodInfo? method = type.GetMethod(
                name: "InstanceInit",
                bindingAttr:
                System.Reflection.BindingFlags.Static
                | System.Reflection.BindingFlags.DeclaredOnly
                | System.Reflection.BindingFlags.NonPublic
            );
            
            if (method is null)
                return;
            
            method.Invoke(null, new object[] {instance, gClass});
        };
        
        /// <summary>
        /// Returns the MethodInfo for the 'GetGType()' function
        /// if the type in question implements it (i.e. a wrapper)
        /// </summary>
        private static Object.TypeDescriptor? GetTypeDescriptor(this System.Type type)
        {
            if (DescriptorDict.TryGetValue(type, out Object.TypeDescriptor? cachedDescriptor))
                return cachedDescriptor;

            FieldInfo? descriptorField = type.GetField(
                name: "GTypeDescriptor",
                bindingAttr:
                System.Reflection.BindingFlags.NonPublic
                | System.Reflection.BindingFlags.Static
                | System.Reflection.BindingFlags.DeclaredOnly
            );

            var descriptor = (Object.TypeDescriptor?) descriptorField?.GetValue(null);
            DescriptorDict[type] = descriptor;

            return descriptor;
        }
        #endregion
    }
}
