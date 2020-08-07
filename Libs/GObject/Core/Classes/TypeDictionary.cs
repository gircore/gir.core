using System;
using System.Collections.Generic;

namespace GObject
{
    public class TypeDictionary
    {
        static readonly Dictionary<Type, ulong> typedict = new Dictionary<Type, ulong>();

        // GObject GetType() function
        public delegate ulong GetTypeDelegate();

        public static bool Contains(Type type) => typedict.ContainsKey(type);

        public static void RegisterType(Type type, ulong typeid)
            => typedict[type] = typeid;

        // Ensures that the object has a corresponding GType
        // For GObjects, this will enforce class creation
        // public static void EnsureType(System.Type type) => gtypeof(type);

        public static ulong gtypeof(System.Type type)
        {
            if (typedict.TryGetValue(type, out ulong typeid))
                return typeid;

            // Register GObject
            if (typeof(GObject.Object).IsAssignableFrom(type))
                Object.RegisterType(type);

            // lookup final time
            if (typedict.TryGetValue(type, out typeid))
                return typeid;

            throw new NotImplementedException("gtypeof lookup");
        }
    }
}