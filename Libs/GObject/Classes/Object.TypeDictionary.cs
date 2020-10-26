using System;
using System.Collections.Generic;

namespace GObject
{
    public partial class Object
    {
        /// <summary>
        /// Type Dictionary for mapping C#'s System.Type
        /// and GLib GTypes (currently Sys.Type, although
        /// this might change)
        /// </summary>
        internal static class TypeDictionary
        {
            #region Fields

            // Dual dictionaries for looking up types and gtypes
            private static readonly Dictionary<System.Type, Type> TypeDict;
            private static readonly Dictionary<Type, System.Type> GTypeDict;
            private static readonly Dictionary<System.Type, TypeDescriptor?> DescriptorDict;

            #endregion

            #region Constructors

            static TypeDictionary()
            {
                // Initialise Dictionaries
                TypeDict = new Dictionary<System.Type, Type>();
                GTypeDict = new Dictionary<Type, System.Type>();
                DescriptorDict = new Dictionary<System.Type, TypeDescriptor?>();

                // Add GObject and GInitiallyUnowned
                Add(typeof(Object), GTypeDescriptor.GType);
                Add(typeof(InitiallyUnowned), GTypeDescriptor.GType);
            }

            #endregion

            #region Methods

            /// <summary>
            /// Add to type dictionary.
            /// </summary>
            /// <param name="type">The C# type.</param>
            /// <param name="gtype">The corresponding GType.</param>
            internal static void Add(System.Type type, Type gtype)
            {
                if (TypeDict.ContainsKey(type) || GTypeDict.ContainsKey(gtype))
                    return;

                TypeDict.Add(type, gtype);
                GTypeDict.Add(gtype, type);
            }

            /// <summary>
            /// Get the C# type from the given GType.
            /// </summary>
            /// <param name="gtype">The GType.</param>
            /// <returns>
            /// An instance of <see cref="System.Type"/> corresponding to the C# type of
            /// the given <paramref name="gtype"/>.
            /// </returns>
            internal static System.Type Get(Type gtype)
            {
                // Check Type Dictionary
                if (GTypeDict.TryGetValue(gtype, out System.Type? type))
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
                    var parent = Global.type_parent(gtype.Value);
                    if (parent == 0)
                        throw new Exception("Could not get Type from GType");

                    // TODO: One-way registration?

                    gtype = new Type(parent);
                }

                return GTypeDict[gtype];
            }

            /// <summary>
            /// Get the GType from the given C# Type.
            /// </summary>
            /// <param name="type">The C# type.</param>
            /// <returns>
            /// An instance of <see cref="Type"/> corresponding to the given C# <paramref name="type"/>.
            /// </returns>
            internal static Type Get(System.Type type)
            {
                // Check Type Dictionary
                if (TypeDict.TryGetValue(type, out Type cachedGtype))
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
                    return TypeDict[type];
                }

                // We are a wrapper, so register types recursively
                Console.WriteLine("Registering Recursively");
                System.Type baseType = type;
                while (!Contains(baseType))
                {
                    Console.WriteLine(baseType.Name);
                    TypeDescriptor? typeDescriptor = GetTypeDescriptor(baseType);

                    if (typeDescriptor is null)
                        throw new ArgumentException($"{type.Name} is unknown.", nameof(type));

                    // Add to typedict for future use
                    Add(baseType, typeDescriptor.GType);
                    Console.WriteLine($"Adding {baseType.Name}");

                    baseType = baseType.BaseType;
                }

                // Return gtype for *this* type
                return TypeDict[type];
            }

            // Contains functions
            internal static bool Contains(System.Type type) => TypeDict.ContainsKey(type);
            internal static bool Contains(Type gtype) => GTypeDict.ContainsKey(gtype);

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
                if (DescriptorDict.TryGetValue(type, out TypeDescriptor? cachedDescriptor))
                    return cachedDescriptor;

                System.Reflection.FieldInfo? descriptorField = type.GetField(
                    nameof(GTypeDescriptor),
                    System.Reflection.BindingFlags.NonPublic
                    | System.Reflection.BindingFlags.Static
                    | System.Reflection.BindingFlags.DeclaredOnly
                );

                var descriptor = (TypeDescriptor?) descriptorField?.GetValue(null);
                DescriptorDict[type] = descriptor;

                return descriptor;
            }

            #endregion
        }
    }
}