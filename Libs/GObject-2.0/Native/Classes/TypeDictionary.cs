using System;
using System.Collections.Generic;

namespace GObject.Native
{
    public static class TypeDictionary
    {
        private static readonly Dictionary<System.Type, Type> _systemTypeDict = new();

        public static void Add(System.Type systemType, Type type)
        {
            _systemTypeDict[systemType] = type;
        }

        public static Type GetGType(System.Type type)
        {
            if (!_systemTypeDict.TryGetValue(type, out Type result))
                throw new Exception($"Can not find native type for system type {type.FullName}. Is the type registered?");
            
            // TODO: Check if subclass via lack of TypeDescriptor
            // If so, register us as a new type

            return result;
        }
    }
}
