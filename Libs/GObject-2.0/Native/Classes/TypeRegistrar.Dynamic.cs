using System.Diagnostics;

namespace GObject.Native
{
    public class DynamicRegistrar : TypeRegistrar
    {
        private static string QualifyName(System.Type type)
            => type.FullName?.Replace(".", "") ?? type.Name;
        
        public void RegisterSubclassDynamic(System.Type type)
        {
            Debug.Assert(
                condition: type.BaseType != null,
                message: "Cannot register a toplevel type - it must inherit from at least GObject"
            );
            
            System.Type baseType = type.BaseType;
            if (!TypeDictionary.ContainsSystemType(baseType))
                RegisterSubclassDynamic(baseType);
            
            // Do actual registration
            RegisterSubclass(
                type: type,
                parentType: baseType,
                qualifiedName: QualifyName(type)
            );
        }
    }
}
