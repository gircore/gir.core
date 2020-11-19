using System;
using System.Collections.Generic;
using System.Reflection;

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
            // TODO: Can we remove one of our typedicts if we are using
            // the DescriptorDict? (i.e. Save Memory)
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
                AddSingle(typeof(Object), GTypeDescriptor.GType);
                AddSingle(typeof(InitiallyUnowned), GTypeDescriptor.GType);
            }

            #endregion

            #region Methods

            /// <summary>
            /// Adds a single pair of (<c>System.Type</c>, <c>Type</c>) to the type dictionary.
            /// Prefer <see cref="AddRecursive(System.Type, Type)"/> where relevant.
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
            /// Recursively register <c>type</c> in the type dictionary. Prefer
            /// <see cref="AddRecursive(System.Type, Type)"/> if the gtype is already known.
            /// </summary>
            /// <param name="type">The C# type.</param>
            internal static void AddRecursive(System.Type type)
            {
                Type gtype = GetTypeDescriptor(type)!.GType;
                AddRecursive(type, gtype);
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

                    TypeDescriptor? typeDescriptor = GetTypeDescriptor(baseType);

                    if (typeDescriptor is null)
                        throw new ArgumentException($"{type.FullName} does not have a type descriptor.", nameof(type));

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
            internal static System.Type Get(Type gtype)
            {
                // TODO: Rework the type dictionary. This kind of assembly search is
                // both poorly designed and expensive. We should switch to a 'type-map'
                // system like Xamarin, as we know Type-GType mappings at compile time.
                
                // We should set assembly metadata to indicate which library
                // the assembly is wrapping. Therefore, we can reliably load (e.g.
                // GtkSourceView could clash with Gtk3 if we load by name).

                // This is a brief explanation of how type lookup currently works:
                //
                // 1. WrapPointerAs<T>(...) => Register the type parameter recursively
                // 2. WrapPointerAs<T>(...) => If the type of the pointer matches the
                //       descriptor of the Type Parameter, then return
                // 3. TypeDict.Get(...) => Check registered types.
                // 4. TypeDict.FuzzySearchLoaded(...) => Searches loaded assemblies for
                //       namespaces that match the first 'Word' of the GType name. If
                //       multiple assemblies match, check second word, etc. Perform type
                //       lookup on returned Assemblies.
                // 5. TypeDict.FuzzySearchUnloaded(...) => Repeat 4) for unloaded
                //       assemblies. Search unloaded for namespaces that match the first
                //       'Word'. If match, keep assembly loaded. If not, unload assembly.
                //       Search all returned assemblies for the necessary type.
                // 6. Worst Case, throw exception or return minimum supported type (e.g.
                //       GObject).
                //
                // Functions:
                // - GetNamespace(string typeName, int depth)
                //       Gets the first 'depth' words from typeName, assuming it is
                //       in camel case.
                // - Get(GObject.Type gtype)
                //       Gets native name from GLib. Checks whether each assembly's
                //       metadata class starts with the first 'Word', and progressively
                //       narrows it down to one assembly by using the second, third, etc
                //       word. Then repeats for unloaded assemblies.
                // - FuzzySearchAssemblies(string typeName, Assembly[] assemblies)
                //       Searches each assembly in the array for Type 'typeName'.
                //       Check Type Dictionary

                // Step 1: Check already registered types
                if (GTypeDict.TryGetValue(gtype, out System.Type? type))
                    return type;

                // It is quite unlikely that we will need to perform a lookup
                // for a type we haven't already registered. Most calls to Get(gtype)
                // will originate from WrapPointerAs<T> which will register 'T' before
                // calling. Therefore, this shouldn't be too prohibitively expensive.

                // Step 2: Search Loaded Assemblies

                // DEBUG: List all loaded assemblies
                DebugPrintLoadedAssemblies();

                // Quick Path: Lookup type using type name and assembly to
                // find an exact match. Hopefully this will happen in 80% of
                // lookup cases. We can maybe optimise this further by looking
                // up the assembly too rather than iterating.

                // Get assemblies starting from most recently used
                Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
                Array.Reverse(assemblies);

                // Search assemblies for the exact matching type
                System.Type? foundType = FuzzySearchAssemblies(gtype, assemblies);

                if (foundType != null)
                {
                    AddRecursive(foundType, gtype);
                    return foundType;
                }

                // TODO: Bulletproof this - Might not work properly
                // GetGTypeNameComponents(gtype, out var asmName, out var typeName);
                // foundType = SearchAssembliesForGType(asmName, typeName);
                // if (foundType != null)
                // {
                //     Console.WriteLine($"Found type. Using {foundType.FullName} = {gtype.ToString()}");
                //     AddRecursive(foundType, gtype);
                //     return foundType;
                // }

                // We are unable to find an exact corresponding System.Type for this
                // objects GType. Look though the type's inheritance chain to
                // find the first type that matches (e.g. GdkScreen for
                // GdkWin32Screen). Effectively, the lowest-common-denominator
                // of functionality will be exposed (which is fine in *most* cases).
                while (!Contains(gtype))
                {
                    var parent = Global.Native.type_parent(gtype.Value);
                    if (parent == 0)
                        throw new Exception($"Could not get Type from GType {gtype.ToString()}");

                    gtype = new Type(parent);

                    // Using the parent gtype, search for compatible System.Type.
                    // PrintGTypeLookup(gtype);
                    // GetGTypeNameComponents(gtype, out asmName, out typeName);
                    // foundType = SearchAssembliesForGType(asmName, typeName);

                    foundType = FuzzySearchAssemblies(gtype, assemblies);
                    if (foundType != null)
                    {
                        Console.WriteLine(
                            $"The System.Type for GType {gtype.ToString()} could not be found. Resorting to using type {foundType.FullName}. Unexpected behaviour may occur");
                        
                        AddRecursive(foundType, gtype);
                        return foundType;
                    }
                }

                // TODO: Search through unloaded assemblies?
                Console.WriteLine(
                    $"Could not find the type {gtype.ToString()}. Returning a GObject. Unloaded assemblies were not searched.");

                // All else fails, return a GObject
                // Should we throw an exception?
                return typeof(GObject.Object);
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
                AddRecursive(type, GetTypeDescriptor(type)!.GType);

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
            internal static TypeDescriptor? GetTypeDescriptor(System.Type type)
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

            private static System.Type? FuzzySearchAssemblies(Type gtype, Assembly[] assemblies)
            {
                // NOTE: At present, the assembly name must contain the prefix (e.g. libhandy's
                // assembly MUST be called Hdy to work). This is a major flaw with this approach
                // which should be fixed by the type-map.

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
                Console.WriteLine("FuzzySearchAssemblies: Looking up" + gtype.ToString() + "with components:");
                foreach (string word in words)
                    Console.WriteLine(" * " + word);

                // We must have at least two "words" to perform a lookup (one word implies the
                // type is not prefixed, which we do not support).
                if (words.Length <= 1)
                {
                    Console.WriteLine($"Non prefixed type name {gtype.ToString()} - This is an error!");
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
                        if (!asm.FullName.StartsWith(asmName))
                            continue;

                        // DEBUG: Print out found match
                        Console.WriteLine(asm.FullName + " matches " + asmName);
                        
                        // Attempt to lookup type in assembly
                        System.Type? foundType = asm.GetType(asmName + "." + typeName);

                        // Go to next assembly if type not found
                        if (foundType is null)
                            continue;

                        // Ensure the GType matches the provided type. Return if true
                        if (GetTypeDescriptor(foundType)?.GType.Equals(gtype) ?? false)
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
                    if (asm.FullName.StartsWith("GLib"))
                        return asm.GetType($"GLib.{typeName}");
                    
                    // GObject
                    if (asm.FullName.StartsWith("GObject"))
                        return asm.GetType($"GObject.{typeName}");
                    
                    // Gio
                    if (asm.FullName.StartsWith("Gio"))
                        return asm.GetType($"Gio.{typeName}");
                }

                return null;
            }

            // private static System.Type? SearchAssembliesForGType(string nspace, string type)
            // {
            //     // TODO: Unloaded assemblies are not considered
            //     var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            //     Array.Reverse(assemblies);
            //     
            //     // Console.WriteLine($"SearchAssembliesForGType: Namespace: {nspace}, Type: {type}");
            //
            //     // TODO: Assembly Name is "G" when using GLib/GObject/Gio
            //     // This breaks the lookup - Fix this
            //
            //     foreach (var assembly in assemblies)
            //     {
            //         var testType = assembly.GetType(nspace + "." + type);
            //         if (testType != null)
            //         {
            //             Console.WriteLine($"SearchAssembliesForGType: Found Type: {testType.FullName}, Namespace: {testType.Namespace}");
            //             return testType;
            //         }
            //     }
            //
            //     return null;
            // }

            // private static System.Type? MatchGTypeForAssemblies(Type gtype)
            // {
            //     var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            //     Array.Reverse(assemblies);
            //
            //     foreach (var assembly in assemblies)
            //     {
            //         foreach (var testType in assembly.GetExportedTypes())
            //         {
            //             if ((typeof(GObject.Object)).IsAssignableFrom(testType))
            //             {
            //                 var desc = GetTypeDescriptor(testType);
            //                 if (desc?.GType.Equals(gtype) ?? false)
            //                 {
            //                     return testType;
            //                 }
            //             }
            //         }
            //     }
            //
            //     return null;
            // }

            private static string[] GetWords(Type gtype)
            {
                return System.Text.RegularExpressions.Regex.Split(gtype.ToString(), @"(?<!^)(?=[A-Z])");
                
                // Gets the name of the GType and splits
                // it into "Words", where each word starts
                // with a capital letter.
                // var gtypeName = gtype.ToString();
                //
                // List<string> words = default!;
                //
                // var lastIndex = 0;
                // var curIndex = 0;
                // foreach (char c in gtypeName)
                // {
                //     if (Char.IsUpper(c) && curIndex != 0)
                //     {
                //         words.Add(gtypeName.Substring(lastIndex, curIndex));
                //         lastIndex = curIndex;
                //     }
                //
                //     curIndex += 1;
                // }
                //
                // return words.ToArray();
            }

            // private static void GetGTypeNameComponents(Type gtype, out string asmName, out string typeName)
            // {
            //     // FIXME: Very hacky way of finding the namespace
            //     // THIS WILL NOT WORK FOR NAMESPACES MORE THAN ONE WORD
            //     // We should migrate to an assembly-metadata based system
            //     // sooner rather than later.
            //     var gtypeName = gtype.ToString();
            //     int index = 0;
            //     foreach (char c in gtypeName)
            //         if (Char.IsUpper(c) && (index != 0))
            //             break;
            //         else
            //             index += 1;
            //
            //     asmName = gtypeName.Substring(0, index); // 0 to index
            //     typeName = gtypeName.Substring(index); // index to end
            // }

            // private static void PrintGTypeLookup(Type gtype)
            // {
            //     GetGTypeNameComponents(gtype, out var asmName, out var typeName);
            //     Console.WriteLine($"Assembly: {asmName}, Type: {typeName}");
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
