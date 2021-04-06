using System;
using System.Collections.Generic;

namespace GObject.Native
{
    public static class TypeDictionary
    {
        private static Dictionary<System.Type, Type> systemTypeDict = new();
        private static Dictionary<Type, System.Type> gobjectTypeDict = new();

        public static void Add(System.Type systemType, TypeDescriptor typeDescriptor)
        {
            var type = typeDescriptor.GType;
            systemTypeDict[systemType] = type;
            gobjectTypeDict[type] = systemType;
        }

        public static Type GetGType(System.Type type)
        {
            if (!systemTypeDict.TryGetValue(type, out Type result))
                throw new Exception($"Can not find native type for system type {type.FullName}. Is the type registered?");

            return result;
        }
    }
}
