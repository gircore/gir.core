using System.Collections.Generic;

namespace GObject
{
    public partial class Object
    { 
        public static class TypeDictionary
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
                TypeDict = new Dictionary<System.Type, Type>();
                GTypeDict = new Dictionary<Type, System.Type>();
                DescriptorDict = new Dictionary<System.Type, TypeDescriptor?>();
                
                Register(typeof(Object), GTypeDescriptor.GType);
                Register(typeof(InitiallyUnowned), GTypeDescriptor.GType);
            }

            #endregion
            
            #region Methods
            /// <summary>
            /// Add to type dictionary.
            /// </summary>
            /// <param name="type">The C# type.</param>
            /// <param name="gtype">The corresponding GType.</param>
            public static void Register(System.Type type, Type gtype)
            {
                if (TypeDict.ContainsKey(type) || GTypeDict.ContainsKey(gtype))
                    return;

                TypeDict.Add(type, gtype);
                GTypeDict.Add(gtype, type);
            }
            
            public static bool Contains(System.Type type) => TypeDict.ContainsKey(type);
            public static bool Contains(Type gtype) => GTypeDict.ContainsKey(gtype);
            
            // <summary>
            /// Get the C# type from the given GType.
            /// </summary>
            /// <param name="gtype">The GType.</param>
            /// <returns>
            /// An instance of <see cref="System.Type"/> corresponding to the C# type of
            /// the given <paramref name="gtype"/>.
            /// </returns>
            public static System.Type? Get(Type gtype)
                => GTypeDict.TryGetValue(gtype, out System.Type? type) ? type : default;

            /// <summary>
            /// Get the GType from the given C# Type.
            /// </summary>
            /// <param name="type">The C# type.</param>
            /// <returns>
            /// An instance of <see cref="Type"/> corresponding to the given C# <paramref name="type"/>.
            /// </returns>
            public static Type? Get(System.Type type)
                => TypeDict.TryGetValue(type, out Type cachedGtype) ? cachedGtype : (Type?) null;

            public static bool TryGet(System.Type type, out Type gtype)
                => TypeDict.TryGetValue(type, out gtype);

            #endregion
        }
    }
}
