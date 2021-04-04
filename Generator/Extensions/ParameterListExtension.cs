using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Repository;
using Repository.Model;

namespace Generator
{
    internal static class ParameterListExtension
    {
        public static string WriteManaged(this ParameterList parameterList, Namespace currentNamespace)
        {
            // Exclude "userData" parameters
            IEnumerable<Parameter> args = parameterList.SingleParameters.Where(x => x.ClosureIndex == null);

            if (parameterList.InstanceParameter is { })
                args = args.Append(parameterList.InstanceParameter);

            return string.Join(", ", args.Select(x => x.Write(Target.Managed, currentNamespace)));
        }

        public static string WriteNative(this ParameterList parameterList, Namespace currentNamespace, bool useSafeHandle = true)
        {
            var offset = parameterList.InstanceParameter is { } ? 1 : 0;
            var parameters = new List<string>();
            foreach (var parameter in parameterList.GetParameters())
            {
                var attribute = GetAttribute(parameter, Target.Native, offset);
                parameters.Add(attribute + parameter.Write(Target.Native, currentNamespace, useSafeHandle));
            }
            
            return string.Join(", ", parameters);
        }
        
        private static string GetAttribute(Parameter parameter, Target target, int offset)
        {
            if (target == Target.Managed)
                return "";
            
            var attribute = parameter.TypeInformation.Array.GetMarshallAttribute(offset);
            
            if (attribute.Length > 0)
                attribute += " ";

            return attribute;
        }
        
        public static string WriteSignalArgsProperties(this ParameterList parameterList, Namespace currentNamespace)
        {
            var builder = new StringBuilder();
            var converter = new CaseConverter(); //TODO Make this a service

            var index = 0;
            foreach (var argument in parameterList.GetParameters())
            {
                index += 1;
                var type = argument.WriteType(Target.Native, currentNamespace);
                var name = converter.ToPascalCase(argument.SymbolName);

                builder.AppendLine($"public {type} {name} => Args[{index}].Extract<{type}>();");
            }

            return builder.ToString();
        }
        
        public static string WriteCallbackMarshaller(this ParameterList parameterList, ReturnValue returnValue, Namespace currentNamespace)
        {
            var builder = new StringBuilder();
            var args = new List<string>();

            foreach (Parameter arg in parameterList.GetParameters())
            {
                // Skip 'user_data' parameters (for callbacks, when closure index is not zero)
                if (arg is SingleParameter {ClosureIndex: { } })
                    continue;

                builder.AppendLine(arg.WriteMarshalArgumentToManaged(currentNamespace));
                args.Add(arg.SymbolName + "Managed");
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
