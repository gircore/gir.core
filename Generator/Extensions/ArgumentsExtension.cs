using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Repository;
using Repository.Model;

namespace Generator
{
    internal static class ArgumentsExtension
    {
        private static IEnumerable<Argument> GetClosureArgs(this IEnumerable<Argument> arguments)
        {
            // TODO: This might be a bit too enthusiastic with removing arguments - Do some testing..?
            
            // Find the index of each 'userData' argument
            IEnumerable<int> closureIds = arguments
                .Where(x => x.ClosureIndex.HasValue)
                .Select(x => x.ClosureIndex.Value);

            // Lookup arguments by index
            return closureIds.Select(x => arguments.ElementAtOrDefault(x) ?? null);
        }

        public static IEnumerable<Argument> GetManagedArgs(this IEnumerable<Argument> arguments)
        {
            // Exclude from generation
            return arguments
                .Except(arguments.GetClosureArgs());
        }

        public static string WriteManaged(this IEnumerable<Argument> arguments, Namespace currentNamespace)
        {
            // Get managed arguments only (excludes userData, etc)
            IEnumerable<Argument> args = GetManagedArgs(arguments);
            
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
        
        public static string WriteCallbackMarshaller(this IEnumerable<Argument> arguments, ReturnValue returnValue, Namespace currentNamespace)
        {
            var builder = new StringBuilder();
            var args = new List<string>();

            foreach (Argument arg in arguments)
            {
                // Skip 'user_data' parameters (for callbacks, when closure index is not zero)
                if (arg.ClosureIndex.HasValue)
                    continue;

                builder.AppendLine(arg.WriteMarshalArgumentToManaged(currentNamespace));
                args.Add(arg.ManagedName + "Managed");
            }

            var funcArgs = string.Join(
                separator: ", ", 
                values: args
            );

            var funcCall = returnValue.IsVoid()
                ? $"managedCallback({funcArgs});" 
                : $"var managed_callback_result = managedCallback({funcArgs});";

            builder.Append(funcCall);

            return builder.ToString();
        }
    }
}
