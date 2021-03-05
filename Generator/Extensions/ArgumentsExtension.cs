using System.Collections.Generic;
using System.Linq;
using System.Text;
using Repository;
using Repository.Model;

namespace Generator
{
    public static class ArgumentsExtension
    {
        public static string WriteManaged(this IEnumerable<Argument> arguments)
        {
            var args = arguments
                .Where(x => x.ClosureIndex == null) // Exclude "userData" parameters
                .Select(x => x.WriteManaged());
            
            return string.Join(", ", args);
        }
        
        public static string WriteNative(this IEnumerable<Argument> arguments, Namespace currentNamespace)
        {
            var args = new List<string>();
            foreach (var argument in arguments)
            {
                var builder = new StringBuilder();

                builder.Append(argument.WriteNativeType(currentNamespace));
                builder.Append(' ');
                builder.Append(argument.NativeName);

                args.Add(builder.ToString());
            }

            return string.Join(", ", args);
        }
        
        public static string WriteSignalArgsProperties(this IEnumerable<Argument> arguments)
        {
            var builder = new StringBuilder();
            var converter = new CaseConverter(); //TODO Make this a service

            var index = 0;
            foreach (var argument in arguments)
            {
                index += 1;
                var type = argument.WriteManaged();
                var name = converter.ToPascalCase(argument.ManagedName);

                builder.AppendLine($"public {type} {name} => Args[{index}].Extract<{type}>();");
            }

            return builder.ToString();
        }
    }
}
