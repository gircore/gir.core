using System;
using System.Runtime.InteropServices;

namespace GObject
{
    public partial class Object
    {
        internal static class TypeRegistration
        {
            #region Methods

            public static Type Register(System.Type type)
            {
                //TODO: Register recursively to save CPU cylcles instead
                //to register only the required types?

                if (TypeDictionary.TryGet(type, out Type gtype))
                    return gtype;

                TypeDescriptor? typeDescriptor = type.GetTypeDescriptor();

                if (typeDescriptor is { })
                {
                    //Type is a native class
                    TypeDictionary.Register(type, typeDescriptor.GType);
                    return typeDescriptor.GType;
                }
                else
                {
                    //Type is a subclass, because custom type does not have a type descriptor
                    //We create a new type for this.

                    //As a base we use the first native class we find in its hierarchy.
                    System.Type boundarySystemType = GetBoundaryType(type);
                    Type boundaryGtype = Register(boundarySystemType); //Get the boundary GType via registering in case it is unknown.
                    TypeQuery query = boundaryGtype.QueryType();

                    // Create TypeInfo
                    var typeInfo = new TypeInfo(
                        class_size: (ushort) query.class_size,
                        instance_size: (ushort) query.instance_size,
                        class_init: type.GetClassInitFunc(),
                        instance_init: type.GetInstanceInitFunc(),
                        base_init: type.GetBaseInitFunc()
                    );

                    // Convert to Pointer
                    IntPtr typeInfoPtr = Marshal.AllocHGlobal(Marshal.SizeOf(typeInfo));
                    Marshal.StructureToPtr(typeInfo, typeInfoPtr, true);

                    // Perform Registration
                    var qualifiedName = QualifyName(type);
                    Console.WriteLine($"Registering type {type.Name} as {qualifiedName}");
                    var typeid = Global.Native.type_register_static(boundaryGtype.Value, qualifiedName, typeInfoPtr, 0);

                    if (typeid == 0)
                        throw new Exception("Type Registration Failed!");

                    // Free Memory
                    Marshal.FreeHGlobal(typeInfoPtr);

                    var subclassType = new Type(typeid);

                    // Register type in type dictionary
                    TypeDictionary.Register(type, subclassType);
                    
                    //Call class init for complete hierarchy
                    //Object & InitallyUnowned are the root and do not have custom code in ClassInit.
                    
                    //TODO If we have subclass1 -> subclass2 -> Button -> Box -> ...
                    //The following will the button class init method 2 times, but it should
                    //only be one time!!!
                    System.Type? parent = type.BaseType;
                    while (parent is { } && (parent != typeof(Object) && (parent != typeof(InitiallyUnowned))))
                    {
                        var parentType = Register(parent);
                        parent.InvokeClassInitMethod(subclassType, type);
                        parent = parent.BaseType;
                    }
                    
                    return subclassType;
                }
            }

            /// <summary>
            /// This returns the System.Type for the furthest derived
            /// wrapper class. It is the boundary between types defined in
            /// GLib (wrappers) and types defined by the user (subclass)
            /// </summary>
            private static System.Type GetBoundaryType(System.Type type)
            {
                while (type.IsSubclass())
                    type = type.BaseType!;

                return type;
            }

            private static string QualifyName(System.Type type)
                => $"{type.Namespace}_{type.Name}".Replace(".", "_");

            #endregion
        }
    }
}
