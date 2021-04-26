using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace GObject.Native
{
    public static class TypeDictionary
    {
        private static readonly Dictionary<System.Type, Type> _systemTypeDict = new();
        private static readonly Dictionary<Type, System.Type> _reverseTypeDict = new();

        private static DynamicRegistrar DynamicRegistrar { get; } = new();
        public static StaticRegistrar StaticRegistrar { get; } = new();

        public static void Add(System.Type systemType, Type type)
        {
            _systemTypeDict[systemType] = type;
            _reverseTypeDict[type] = systemType;
        }
        
        internal static Type GetGType(System.Type type)
        {
            // Check type is a GObject
            // This should never fail as GetGType is only called from within Object.cs
            Debug.Assert(
                condition: type.IsAssignableTo(typeof(GObject.Object)),
                message: $"Parameter {type} is not a GObject or subclass of GObject"
            );

            // Have we already registered the type?
            if (!_systemTypeDict.ContainsKey(type))
            {
                // We are not in the type dictionary, which means we are
                // an unregistered managed subclass type. Therefore, we should
                // register ourselves and every type we inherit from that has also
                // not been registered.
                DynamicRegistrar.RegisterSubclassDynamic(type);
            }

            return _systemTypeDict[type];
        }
        
        internal static System.Type GetSystemType(Type gtype)
        {
            if (_reverseTypeDict.TryGetValue(gtype, out System.Type? sysType))
                return sysType;

            // If gtype is not in the type dictionary, walk up the
            // tree until we find a type that is. As all objects are
            // descended from GObject, we will eventually find a parent
            // type that is registered.
            
            while (!_reverseTypeDict.TryGetValue(gtype, out sysType))
                gtype = new Type(Functions.TypeParent(gtype.Value));

            // Store for future lookups
            _reverseTypeDict[gtype] = sysType;

            return sysType;
        }

        internal static bool ContainsGType(Type gtype)
            => _reverseTypeDict.ContainsKey(gtype);
        
        internal static bool ContainsSystemType(System.Type type)
            => _systemTypeDict.ContainsKey(type);
    }
}
