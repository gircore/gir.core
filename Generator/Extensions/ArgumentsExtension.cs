using System.Collections.Generic;
using System.Linq;
using System.Text;
using Repository;
using Repository.Model;

namespace Generator
{
    internal static class ArgumentsExtension
    {
        public static string WriteManaged(this IEnumerable<Argument> arguments, Namespace currentNamespace)
        {
            var args = arguments.Where(x => x.ClosureIndex == null); // Exclude "userData" parameters

            return string.Join(", ", args.Select(x => x.WriteManaged(currentNamespace)));
        }
        
        public static string WriteNative(this IEnumerable<Argument> arguments, Namespace currentNamespace)
        {
            var args = new List<string>();
            foreach (var argument in arguments)
            {
                args.Add(argument.WriteNative(currentNamespace));
            }

            return string.Join(", ", args);
        }
        
        public static string WriteSignalArgsProperties(this IEnumerable<Argument> arguments, Namespace currentNamespace)
        {
            var builder = new StringBuilder();
            var converter = new CaseConverter(); //TODO Make this a service

            var index = 0;
            foreach (var argument in arguments)
            {
                index += 1;
                var type = argument.WriteManagedType(currentNamespace);
                var name = converter.ToPascalCase(argument.ManagedName);

                builder.AppendLine($"public {type} {name} => Args[{index}].Extract<{type}>();");
            }

            return builder.ToString();
        }
    }
}
