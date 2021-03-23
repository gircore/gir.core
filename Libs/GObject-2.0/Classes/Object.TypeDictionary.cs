using System;
using System.Collections.Generic;
using System.Reflection;

namespace GObject
{
    public partial class Object
    {
        /// <summary>
        /// Type Dictionary for mapping C#'s System.Type
        /// and GLib GTypes.
        /// </summary>
        internal protected static class TypeDictionary
        {
            #region Fields

            // Dual dictionaries for looking up types and gtypes
            // TODO: Transition to using just a GType->Type dictionary
            private static readonly Dictionary<System.Type, Type> TypeDict;
            private static readonly Dictionary<Type, System.Type> GTypeDict;

            #endregion

            #region Constructors

            static TypeDictionary()
            {
                // Initialise Dictionaries
                TypeDict = new Dictionary<System.Type, Type>();
                GTypeDict = new Dictionary<Type, System.Type>();


                // Add GObject and GInitiallyUnowned
                AddSingle(typeof(Object), GTypeDescriptor.GType);
                AddSingle(typeof(InitiallyUnowned), GTypeDescriptor.GType);
            }

            #endregion

            #region Methods

            /// <summary>
            /// Adds a single pair of (<c>System.Type</c>, <c>Type</c>) to the type dictionary.
            /// Prefer <see cref="AddRecursive(System.Type, Type)"/> where appropriate.
            /// </summary>
            /// <param name="type">The C# type.</param>
            /// <param name="gtype">The corresponding GType.</param>
            internal static void AddSingle(System.Type type, Type gtype)
            {
                if (TypeDict.ContainsKey(type) || GTypeDict.ContainsKey(gtype))
                    return;

                TypeDict.Add(type, gtype);
                GTypeDict.Add(gtype, type);
            }

            /// <summary>
            /// Adds a one-way mapping from <see cref="GObject.Type"/> to <see cref="System.Type"/>
            /// so that a single C# type can act as a fallback for multiple GObject types.
            /// For example, GdkWin32Screen, GdkX11Screen, and GdkWaylandScreen must all
            /// be represented by a Gdk.Screen object. 
            /// </summary>
            /// <param name="type">The C# type to be registered</param>
            /// <param name="gtype">The GType which aliases the C# type</param>
            internal static void AddAlias(System.Type type, GObject.Type gtype)
            {
                if (!GTypeDict.ContainsKey(gtype))
                    GTypeDict[gtype] = type;
            }

            /// <summary>
            /// Recursively register <c>type</c> in the type dictionary. Prefer
            /// <see cref="AddRecursive(System.Type, Type)"/> if the GType is already known.
            /// </summary>
            /// <param name="type">The C# type.</param>
            internal static void AddRecursive(System.Type type)
            {
                var descriptor = TypeDescriptorRegistry.ResolveTypeDescriptorForType(type);
                AddRecursive(type, descriptor.GType);
            }

            /// <summary>
            /// Recursively register a <c>type</c> and <c>gtype</c> pair in type dictionary. This
            /// is preferred to <see cref="AddRecursive(System.Type)"/> as it avoids an additional
            /// type descriptor lookup.
            /// </summary>
            /// <param name="type">The C# type.</param>
            /// <param name="gtype">The corresponding GType.</param>
            internal static void AddRecursive(System.Type type, Type gtype)
            {
                AddSingle(type, gtype);

                // Register recursively
                System.Type? baseType = type == typeof(GObject.Object) ? null : type.BaseType;
                while (baseType != null && !Contains(baseType))
                {
                    // If GObject, we are the most basic type, so return
                    if (type == typeof(GObject.Object))
                        return;

                    TypeDescriptor typeDescriptor = TypeDescriptorRegistry.ResolveTypeDescriptorForType(baseType);

                    // Add to typedict for future use
                    AddSingle(baseType, typeDescriptor.GType);

                    baseType = baseType.BaseType;
                }
            }

            /// <summary>
            /// Get the C# type from the given GType.
            /// </summary>
            /// <param name="gtype">The GType.</param>
            /// <returns>
            /// An instance of <see cref="System.Type"/> corresponding to the C# type of
            /// the given <paramref name="gtype"/>.
            /// </returns>
            public static System.Type Get(Type gtype)
            {
                // TODO: Rework the type dictionary. This kind of assembly search is
                // both poorly designed and expensive. We should switch to a 'type-map'
                // system like Xamarin, as we know Type-GType mappings at compile time.

                // We should set assembly metadata to indicate which library
                // the assembly is wrapping. Therefore, we can reliably load (e.g.
                // GtkSourceView could clash with Gtk3/Gtk4 if we load by name).

                // This is a brief explanation of how type lookup currently works:

                // Firstly, in Object.WrapHandle<T>(...) we do the following things:

                // - Register the type parameter recursively to fill as much
                //       of the type dictionary as we can.
                //
                // - If the type of the pointer matches the descriptor of the
                //       Type Parameter, we can optimise the lookup out entirely.


                // If we still do not know the type at this point, we must perform a
                // typedict lookup. This function (TypeDictionary.Get) goes as follows:

                // 1. Dictionary Lookup => Check already registered types. This will almost
                //       always be successful.
                //
                // 2. FuzzySearchAssemblies => Searches loaded assemblies for
                //       namespaces that match the first 'Word' of the GType name. If
                //       multiple assemblies match, check second word, etc. Perform type
                //       lookup on found Assemblies.
                //
                // 3. Fallback => Return minimum supported type by walking up the type
                //       hierarchy to find a compatible type. In the worst case, we will
                //       end up returning GObject. This lookup is relatively expensive, so
                //       we want to avoid it where possible.


                // Step 1: Check already registered types
                if (GTypeDict.TryGetValue(gtype, out System.Type? type))
                    return type;


                // Step 2: Search Loaded Assemblies for Exact Match

                // It is quite unlikely that we will need to perform a lookup
                // for a type we haven't already registered. Most calls to Get(gtype)
                // will originate from WrapHandle<T> which will register 'T' before
                // calling. Therefore, this shouldn't be too prohibitively expensive.

                // DEBUG: List all loaded assemblies
                // DebugPrintLoadedAssemblies();

                // Get assemblies starting from most recently used
                Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
                Array.Reverse(assemblies);

                // Search assemblies for the exact matching type
                System.Type? foundType = FuzzySearchAssemblies(gtype, assemblies);

                if (foundType != null)
                {
                    // Register and return if found
                    AddRecursive(foundType, gtype);
                    return foundType;
                }


                // Step 3: Find a compatible fallback type

                // We are unable to find an exact corresponding System.Type for this
                // objects GType. Walk up though the type's inheritance chain to
                // find the first type that matches (e.g. GdkScreen for
                // GdkWin32Screen). Effectively, the lowest-common-denominator
                // of functionality will be exposed (which is fine in *most* cases).

                Type initialGType = gtype;
                while (!Contains(gtype))
                {
                    // Get parent type
                    var parent = Functions.Native.TypeParent(gtype.Value);
                    if (parent == 0)
                    {
                        throw new Exception($"Could not get parent type for GType {gtype}");
                    }
                    gtype = new Type(parent);

                    // Using the parent gtype, search for compatible System.Type.
                    foundType = FuzzySearchAssemblies(gtype, assemblies);

                    // If found, register and break
                    if (foundType != null)
                    {
                        AddRecursive(foundType, gtype);
                        AddAlias(foundType, initialGType);
                        break;
                    }
                }

                // Get return type from typedict (The above loop guarantees a
                // suitable fallback type is registered and available).
                System.Type returnType = foundType ?? GTypeDict[initialGType];

                // Print warning message
                Console.WriteLine($"The System.Type for GType {initialGType} could not be found (Unloaded assemblies were not searched). Resorting to using type {returnType.FullName}. Unexpected behaviour may occur");

                return returnType;
            }

            /// <summary>
            /// Get the GType from the given C# Type.
            /// </summary>
            /// <param name="type">The C# type.</param>
            /// <returns>
            /// An instance of <see cref="Type"/> corresponding to the given C# <paramref name="type"/>.
            /// </returns>
            public static Type Get(System.Type type)
            {
                // Check Type Dictionary
                if (TypeDict.TryGetValue(type, out Type cachedGtype))
                    return cachedGtype;

                // If we are looking up a type that is not yet in
                // the type dictionary, we are most likely registering
                // a new type. Therefore, we should register the type
                // and parent types recursively now, to avoid having to
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

                // We are a wrapper. Add to type dictionary
                TypeDescriptor descriptor = TypeDescriptorRegistry.ResolveTypeDescriptorForType(type);
                AddRecursive(type, descriptor.GType);

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
                   !TypeDescriptorRegistry.TryResolveTypeDescriptorForType(type, out var _);

            // Tries to find Assemblies which match the possible type prefixes of a
            // given gtype. For each assembly which is considered a possible candidate,
            // a type lookup is performed using the prefix as a namespace.
            //
            // NOTE: At present, the assembly name must contain the prefix (e.g. libhandy's
            // assembly MUST be called Hdy to work). This is a major flaw with this approach
            // which should be fixed by the type-map.
            private static System.Type? FuzzySearchAssemblies(Type gtype, Assembly[] assemblies)
            {
                // Break up CamelCase GType name into individual "Words"
                // For example, GdkWin32Screen becomes "Gdk" + "Win32" + "Screen"
                string[] words = GetWords(gtype);

                // HACK: If the type starts with the prefix 'G', it is in one of
                // GLib/GObject/Gio. Therefore, only lookup in those three libraries.
                if (words[0] == "G")
                {
                    // Get type name excluding the prefix 'G' (e.g. GDBusConnection -> DBusConnection)
                    string typeName = String.Join("", words, 1, words.Length - 1);
                    return FuzzySearchGLib(gtype, typeName, assemblies);
                }

                // DEBUG: Print Assembly Lookup
                Console.Write("FuzzySearchAssemblies: Looking up " + gtype + " with components: ");
                foreach (string word in words)
                {
                    if (word == words[words.Length - 1])
                        Console.Write(word + "\n");
                    else
                        Console.Write(word + " | ");
                } // END DEBUG

                // We must have at least two "words" to perform a lookup (one word implies the
                // type is not prefixed, which we do not support).
                if (words.Length <= 1)
                {
                    Console.WriteLine($"Non prefixed type name {gtype} - This is an error!");
                    return null;
                }

                // Start lookup with the first word being the assembly name, and the remainder being the
                // type. Then proceed to using two words as the assembly name, then three, and so on. This
                // is to allow for libraries like GtkSourceView using 'GtkSource' as a prefix, which clashes
                // with 'Gtk' itself.
                for (var n = 1; n < words.Length; n += 1)
                {
                    // Get the Assembly and Type names
                    var asmName = String.Join("", words, 0, n);
                    var typeName = String.Join("", words, n, words.Length - n);

                    // Iterate over assemblies
                    foreach (Assembly asm in assemblies)
                    {
                        // Go to next assembly if it doesn't match the search term
                        var fullname = asm.FullName ?? string.Empty;
                        if (!fullname.StartsWith(asmName))
                            continue;

                        // DEBUG: Print out found match
                        Console.WriteLine(asm.FullName + " matches " + asmName);

                        // Attempt to lookup type in assembly
                        System.Type? foundType = asm.GetType(asmName + "." + typeName);

                        // Go to next assembly if type not found
                        if (foundType is null)
                            continue;

                        // Ensure the GType matches the provided type. Return if true
                        if (TypeDescriptorRegistry.TryResolveTypeDescriptorForType(foundType, out var descriptor))
                            if (descriptor.GType.Equals(gtype))
                                return foundType;
                    }
                }

                // Otherwise return null
                return null;
            }

            private static System.Type? FuzzySearchGLib(Type gtype, string typeName, Assembly[] assemblies)
            {
                // Hack to lookup types in either GLib/GObject/Gio, as they use the
                // prefix 'G'. This is to have a functional typedict in the mean time
                // while the type-map approach is evaluated.
                foreach (Assembly asm in assemblies)
                {
                    // GLib
                    var fullname = asm.FullName ?? string.Empty;
                    if (fullname.StartsWith("GLib"))
                        return asm.GetType($"GLib.{typeName}");

                    // GObject
                    if (fullname.StartsWith("GObject"))
                        return asm.GetType($"GObject.{typeName}");

                    // Gio
                    if (fullname.StartsWith("Gio"))
                        return asm.GetType($"Gio.{typeName}");
                }

                return null;
            }

            private static string[] GetWords(Type gtype)
            {
                var typeName = gtype.ToString();

                var result = new System.Text.StringBuilder();
                foreach (var c in typeName)
                {
                    if (char.IsUpper(c) && result.Length > 0)
                    {
                        result.Append(";");
                    }
                    result.Append(c);
                }
                return result.ToString().Split(';');
            }

            // // TODO: Profile - Is this Regex Version faster or slower?
            // private static string[] GetWords(Type gtype)
            // {
            //     return System.Text.RegularExpressions.Regex.Split(gtype.ToString(), @"(?<!^)(?=[A-Z])");
            // }

            private static void DebugPrintLoadedAssemblies()
            {
                // Print all currently loaded assemblies
                Console.WriteLine($"Loaded Assemblies:");
                foreach (var asm in AppDomain.CurrentDomain.GetAssemblies())
                    Console.WriteLine(" - " + asm.GetName());
            }

            #endregion
        }
    }
}
