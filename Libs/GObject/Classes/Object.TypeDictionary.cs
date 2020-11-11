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
                // TODO: Use Gir Namespace to create a "module initializer"
                // We have name in the form of NamespaceType
                //
                // 1. WrapPointerAs<T>(...) => Register the type parameter recursively
                // 2. WrapPointerAs<T>(...) => If the type of the pointer matches the
                //       descriptor of the Type Parameter, then return
                // 3. TypeDict.Lookup(...) => Check registered types
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
                
                // DEBUG: Log gtype namespace and type
                PrintGTypeLookup(gtype);

                // DEBUG: List all loadeed assemblies
                Console.WriteLine($"Loaded Assemblies:");
                foreach (var asm in AppDomain.CurrentDomain.GetAssemblies())
                    Console.WriteLine(" - " + asm.GetName());



                // TODO: Recursive registration for GTypes so we don't have to
                // repeat this expensive process again.

                // FIXME: For unloaded assemblies, we should set assembly
                // metadata to indicate which library the assembly is wrapping.
                // Therefore, we can reliably load (e.g. GtkSourceView
                // could clash with Gtk3 if we load by name).

                System.Type? foundType;
                
                // Quick Path: Lookup type using type name and assembly to
                // find an exact match. Hopefully this will happen in 80% of
                // lookup cases. We can maybe optimise this further by looking
                // up the assembly too rather than iterating.
                
                // TODO: Bulletproof this - Might not work properly
                GetGTypeNameComponents(gtype, out var asmName, out var typeName);
                foundType = SearchAssembliesForGType(asmName, typeName);
                if (foundType != null)
                {
                    Console.WriteLine($"Found type. Using {foundType.FullName} = {gtype.ToString()}");
                    Add(foundType, gtype);
                    return foundType;
                }

                // Check every single GObject based type's type descriptor in
                // each assembly to find out which type we want.
                // foundType = MatchGTypeForAssemblies(gtype);
                // if (foundType != null)
                // {
                //     Console.WriteLine($"Found type. Using {foundType.FullName} = {gtype.ToString()}");
                //     Add(foundType, gtype);
                //     return foundType;
                // }

                // TODO: This is incredibly expensive. Optimise

                // We are unable to find an exact corresponding System.Type for this
                // objects GType. Look though the type's inheritance chain to
                // find the first type that matches (e.g. GdkScreen for
                // GdkWin32Screen). Effectively, the lowest-common-denominator
                // of functionality will be exposed (which is fine in *most* cases).
                while (!Contains(gtype))
                {
                    var parent = Global.type_parent(gtype.Value);
                    if (parent == 0)
                        throw new Exception($"Could not get Type from GType {gtype.ToString()}");

                    gtype = new Type(parent);

                    // Using the parent gtype, search for compatible System.Type.
                    PrintGTypeLookup(gtype);
                    GetGTypeNameComponents(gtype, out asmName, out typeName);
                    foundType = SearchAssembliesForGType(asmName, typeName);
                    if (foundType != null)
                    {
                        Console.WriteLine($"The System.Type for GType {gtype.ToString()} could not be found. Resorting to using type {foundType.FullName}. Unexpected behaviour may occur");
                        Add(foundType, gtype);
                        return foundType;
                    }
                }

                // TODO: Search through unloaded assemblies?

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

                var descriptor = (TypeDescriptor?)descriptorField?.GetValue(null);
                DescriptorDict[type] = descriptor;

                return descriptor;
            }

            private static System.Type? SearchAssembliesForGType(string nspace, string type)
            {
                // TODO: Unloaded assemblies are not considered
                var assemblies = AppDomain.CurrentDomain.GetAssemblies();
                Array.Reverse(assemblies);
                
                Console.WriteLine($"SearchAssembliesForGType: Namespace: {nspace}, Type: {type}");

                // TODO: Assembly Name is "G" when using GLib/GObject/Gio
                // This breaks the lookup - Fix this

                foreach (var assembly in assemblies)
                {
                    var testType = assembly.GetType(nspace + "." + type);
                    if (testType != null)
                    {
                        Console.WriteLine($"SearchAssembliesForGType: Found Type: {testType.FullName}, Namespace: {testType.Namespace}");
                        return testType;
                    }
                }

                return null;
            }

            private static System.Type? MatchGTypeForAssemblies(Type gtype)
            {
                var assemblies = AppDomain.CurrentDomain.GetAssemblies();
                Array.Reverse(assemblies);

                foreach (var assembly in assemblies)
                {
                    foreach (var testType in assembly.GetExportedTypes())
                    {
                        if ((typeof(GObject.Object)).IsAssignableFrom(testType))
                        {
                            var desc = GetTypeDescriptor(testType);
                            if (desc?.GType.Equals(gtype) ?? false)
                            {
                                return testType;
                            }
                        }
                    }
                }

                return null;
            }

            private static void GetGTypeNameComponents(Type gtype, out string asmName, out string typeName)
            {
                // FIXME: Very hacky way of finding the namespace
                // THIS WILL NOT WORK FOR NAMESPACES MORE THAN ONE WORD
                // We should migrate to an assembly-metadata based system
                // sooner rather than later.
                var gtypeName = gtype.ToString();
                int index = 0;
                foreach (char c in gtypeName)
                    if (Char.IsUpper(c) && (index != 0))
                        break;
                    else
                        index += 1;

                asmName = gtypeName.Substring(0, index); // 0 to index
                typeName = gtypeName.Substring(index); // index to end
            }

            private static void PrintGTypeLookup(Type gtype)
            {
                GetGTypeNameComponents(gtype, out var asmName, out var typeName);
                Console.WriteLine($"Assembly: {asmName}, Type: {typeName}");
            }

            #endregion
        }
    }
}
