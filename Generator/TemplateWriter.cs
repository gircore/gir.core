using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Repository.Analysis;
using Repository.Model;

#nullable enable

namespace Generator
{
    public static class TemplateWriter
    {
        public static string WriteArguments(IEnumerable<Argument> arguments)
        {
            var args = arguments.Select(x => WriteTypeReference(x.Type) + " " + x.Name);
            return string.Join(", ", args);
        }

        public static string WriteTypeReference(ITypeReference typeReference)
        {
            // TODO: More advanced type resolution logic?

            if (typeReference.Type is null)
                throw new InvalidOperationException($"The Type for {typeReference.Name} Reference has not been resolved. It cannot be printed.");

            // Fundamental Type
            if (typeReference.Type.Namespace == null)
                return typeReference.Type.ManagedName;

            // External Array
            if (typeReference.IsForeign && typeReference.IsArray)
                return $"{typeReference.Type.Namespace.Name}.{typeReference.Type.ManagedName}[]";

            // External Type
            if (typeReference.IsForeign)
                return $"{typeReference.Type.Namespace.Name}.{typeReference.Type.ManagedName}";

            // Internal Array
            if (typeReference.IsArray)
                return $"{typeReference.Type.ManagedName}[]";

            // Internal Type
            return typeReference.Type.ManagedName;
        }

        public static string WriteInheritance(ITypeReference? parent, IEnumerable<ITypeReference> implements)
        {
            var builder = new StringBuilder();

            if (parent is { })
                builder.Append(": " + WriteTypeReference(parent));

            var refs = implements.ToList();
            if (refs.Count == 0)
                return builder.ToString();

            if (parent is { })
                builder.Append(", ");
                
            builder.Append(string.Join(", ", refs.Select(WriteTypeReference)));
            return builder.ToString();
        }

        public static string WriteMethod(Method method)
        {
            var returnValue = WriteTypeReference(method.ReturnValue.TypeReference);
            return $"public static extern {returnValue} {method.Name}();\r\n";
        }
    }
}
