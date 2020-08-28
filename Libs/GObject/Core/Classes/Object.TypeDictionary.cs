using System;
using System.Reflection;
using System.Collections.Generic;

namespace GObject
{
    public partial class Object
    {
        // Type Dictionary for mapping C#'s System.Type
        // and GLib GTypes (currently Sys.Type, although
        // this might change)
        internal static class TypeDictionary
        {
            // Dual dictionaries for looking up types and gtypes
            private static readonly Dictionary<System.Type, Sys.Type> typedict;
            private static readonly Dictionary<Sys.Type, System.Type> gtypedict;
            private static readonly Dictionary<System.Type, TypeDescriptor?> descriptordict;

            static TypeDictionary()
            {
                // Initialise Dictionaries
                typedict = new Dictionary<System.Type, Sys.Type>();
                gtypedict = new Dictionary<Sys.Type, System.Type>();
                descriptordict = new Dictionary<System.Type, TypeDescriptor?>();

                // Add GObject and GInitiallyUnowned
                Add(typeof(GObject.Object), Object.GTypeDescriptor.GType.GType);
                Add(typeof(InitiallyUnowned), InitiallyUnowned.GTypeDescriptor.GType.GType);
            }

            // Add to type dictionary
            internal static void Add(System.Type type, Sys.Type gtype)
            {
                if (typedict.ContainsKey(type) ||
                    gtypedict.ContainsKey(gtype))
                    return;

                typedict.Add(type, gtype);
                gtypedict.Add(gtype, type);
            }

            // Get System.Type from GType
            internal static System.Type Get(Sys.Type gtype)
            {
                // Check Type Dictionary
                if (gtypedict.TryGetValue(gtype, out var type))
                    return type;

                // It is quite unlikely that we need to perform a lookup
                // by gtype of a type we haven't created ourselves. Therefore,
                // this shouldn't be too prohibitively expensive.

                // TODO: Revise Generator to automatically implement
                // Use Wrapper Attribute for mapping GType name to C# Type
                // Use GetGType function for reverse mapping.

                // Search all System.Type which contain a [Wrapper(TypeName)]
                // for the corresponding type.

                // Possible Idea: Autogenerate a 'RegisterTypes.cs' file that
                // on startup will add every type to the type dictionary?

                // Quick Path: Find the first 'Word' in the type and lookup
                // assemblies by that name. Do we hardcode references to 'Pango',
                // 'Gtk', etc?

                // foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
                // {
                //     assembly.GetType()
                // }

                // Search through unloaded assemblies?

                // TODO: For now, we'll just look through the type's inheritance chain
                // and find the first already registered type (e.g. GtkWidget). Effectively,
                // the lowest-common-denominator of functionality will be exposed.
                while (!Contains(gtype))
                {
                    ulong parent = Sys.Methods.type_parent(gtype.Value);
                    if (parent == 0)
                        throw new Exception("Could not get Type from GType");

                    // TODO: One-way registration?

                    gtype = new Sys.Type(parent);
                }

                return gtypedict[gtype];
            }

            // Get GType from System.Type
            internal static Sys.Type Get(System.Type type)
            {
                // Check Type Dictionary
                if (typedict.TryGetValue(type, out var cachedGtype))
                    return cachedGtype;

                // If we are looking up a type that is not yet in
                // the type dictionary, we are most likely registering
                // a new type. Therefore, we should register the type
                // and parent types recursively now to avoid having to
                // do this in the future.

                // Retrieve the GType accordingly
                if (IsSubclass(type))
                {
                    // We are a subclass
                    // RegisterNativeType will recursively add this
                    // and all parent types to the type dictionary
                    RegisterNativeType(type);
                    return typedict[type];
                }

                // We are a wrapper, so register types recursively
                Console.WriteLine("Registering Recursively");
                System.Type baseType = type;
                while (!Contains(baseType))
                {
                    Console.WriteLine(baseType.Name);
                    var typeDescriptor = GetTypeDescriptor(baseType);

                    if(typeDescriptor is null)
                        throw new ArgumentException($"{type.Name} is unknown.", nameof(type));
                    
                    // Add to typedict for future use
                    Add(baseType, typeDescriptor.GType.GType);
                    Console.WriteLine($"Adding {baseType.Name}");

                    baseType = baseType.BaseType;
                }

                // Return gtype for *this* type
                return typedict[type];
            }

            // Contains functions
            internal static bool Contains(System.Type type) => typedict.ContainsKey(type);
            internal static bool Contains(Sys.Type gtype) => gtypedict.ContainsKey(gtype);

            // Determines whether the type is a managed subclass,
            // as opposed to wrapping an existing type.
            internal static bool IsSubclass(System.Type type)
                => type != typeof(Object) &&
                type != typeof(InitiallyUnowned) &&
                GetTypeDescriptor(type) is null;

            // Returns the MethodInfo for the 'GetGType()' function
            // if the type in question implements it (i.e. a wrapper)
            private static TypeDescriptor? GetTypeDescriptor(System.Type type)
            {
                if (descriptordict.TryGetValue(type, out var cachedDescriptor))
                    return cachedDescriptor;
                
                var descriptorField = type.GetField(
                    nameof(Object.GTypeDescriptor), 
                    BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.DeclaredOnly
                );
                
                var descriptor = (TypeDescriptor?)descriptorField?.GetValue(null);
                descriptordict[type] = descriptor;
                
                return descriptor;
            }
        }
    }
}
