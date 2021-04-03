using System.Collections.Generic;

namespace GObject.Integration
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
    }
}
