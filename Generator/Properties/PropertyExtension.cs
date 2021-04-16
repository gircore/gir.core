using Repository.Model;

namespace Generator.Properties
{
    public static class PropertyExtension
    {
        public static string GetDescriptorName(this Property property)
            => property.SymbolName + "PropertyDefinition";

        public static string GetTypeName(this Property property, Namespace currentNamespace)
            => property.WriteType(Target.Managed, currentNamespace);
    }
}
