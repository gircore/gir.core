using System;
using System.Collections.Generic;
using System.Text;
using Repository.Model;
using String = Repository.Model.String;

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

            var definition = @$"public static readonly Property<{typeName}> {descriptorName} = Property<{typeName}>.Register<{parentType}>(
    {GetArguments(property)}
);
";
            return CommentIfUnsupported(definition, property);
        }

        private static string GetArguments(Property property)
        {
            var arguments = new List<string>()
            {
                $"nativeName: \"{property.Name}\"",
                $"managedName: nameof({property.SymbolName})"
            };

            if (property.Readable)
                arguments.Add($"get: o => o.{property.SymbolName}");

            if (property.Writeable)
                arguments.Add($"set: (o, v) => o.{property.SymbolName} = v");

            return string.Join(",\r\n    ", arguments);
        }

        public static string WriteProperty(Property property, Namespace currentNamespace)
        {
            var descriptorName = property.GetDescriptorName();
            var typeName = property.GetTypeName(currentNamespace);

            var builder = new StringBuilder();
            builder.AppendLine($"public {typeName} {property.SymbolName}");
            builder.AppendLine("{");

            if (property.Readable)
                builder.AppendLine($"    get => GetProperty({descriptorName});");

            if (property.Writeable)
                builder.AppendLine($"    set => SetProperty({descriptorName}, value);");

            builder.AppendLine("}");

            return CommentIfUnsupported(builder.ToString(), property);
        }

        private static string CommentIfUnsupported(string str, Property property)
        {
            //TODO: Remove this method if all cases are supported
            return property switch
            {
                { SymbolReference: { Symbol: { } and String } } => str,
                { SymbolReference: { Symbol: { } and PrimitiveValueType } } => str,
                { SymbolReference: { Symbol: { } and Enumeration } } => str,
                { SymbolReference: { Symbol: { } and Class } } => str,
                _ => "/*" + str + "*/"
            };
        }
    }
}
