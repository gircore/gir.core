using System;
using GirLoader.Output;
using String = GirLoader.Output.String;

namespace Generator.Properties
{
    public static class PropertyExtension
    {
        public static string GetDescriptorName(this Property property)
            => property.Name + "PropertyDefinition";

        public static string GetTypeName(this Property property, Namespace currentNamespace)
        {
            var type = property.WriteType(Target.Managed, currentNamespace);

            if (property.TypeReference.Type is null)
                throw new Exception($"Can not get type of property {property.OriginalName}. Symbol is missing");

            // Properties need special nullable handling as each C value is implicitly nullable.
            // The C# Marshaller returns default values for primitive value types. But there
            // can be null values for strings and objects, even if the nullability attribute
            // is not present in the gir file
            return property.TypeReference.Type switch
            {
                String => type + "?",
                Class => type + "?",
                _ => type
            };
        }
    }
}
