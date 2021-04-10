using System;
using Repository.Model;

namespace Generator.Properties
{
    public class PropertyGenerator
    {

        public static string WriteDescriptor(Property property, Symbol symbol, Namespace currentNamespace)
        {
            if (symbol.Namespace is null)
                throw new Exception("Can not write property for symbol with unknown namespace");
            
            var typeName = property.GetTypeName(currentNamespace);
            var descriptorName = property.GetDescriptorName();
            var parentType = symbol.Write(Target.Managed, currentNamespace);
            return @$"/*public static readonly Property<{typeName}> {descriptorName} = Property<{typeName}>.Register<{parentType}>(
    nativeName: ""{property.Name}"",
    managedName: nameof({property.SymbolName}),
    get: o => o.{property.SymbolName},
    set: (o, v) => o.{property.SymbolName} = v
);
*/";
        }

        public static string WriteProperty(Property property, Namespace currentNamespace)
        {
            var descriptorName = property.GetDescriptorName();
            var typeName = property.GetTypeName(currentNamespace);
            return @$"/*public {typeName} {property.SymbolName}
{{
    get => GetProperty({descriptorName});
    set => SetProperty({descriptorName}, value);
}}
*/";
        }
    }
}
