using System;
using System.Text;
using Repository.Model;
using Type = Repository.Model.Type;

namespace Generator
{
    internal static class ArgumentExtension
    {
        public static string WriteNative(this Argument argument, Namespace currentNamespace)
            => argument.Write(Target.Native, currentNamespace);
        
        public static string WriteManaged(this Argument argument, Namespace currentNamespace)
            => argument.Write(Target.Managed, currentNamespace);

        private static string Write(this Argument argument, Target target,  Namespace currentNamespace)
        {
            var type = GetFullType(argument, target, currentNamespace);
            
            var builder = new StringBuilder();
            builder.Append(type);
            builder.Append(' ');
            builder.Append(argument.NativeName);

            return builder.ToString();
        }

        private static string GetFullType(this Argument argument, Target target, Namespace currentNamespace)
        {
            var attribute = GetAttribute(argument);
            var direction = GetDirection(argument);
            var type = GetType(argument, target, currentNamespace);
            var nullable = GetNullable(argument);
            
            return $"{attribute}{direction}{type}{nullable}";
        }

        private static string GetAttribute(Argument argument)
        {
            var attribute = argument.Array.GetMarshallAttribute();
            
            if (attribute.Length > 0)
                attribute += " ";

            return attribute;
        }

        private static string GetDirection(Argument argument)
        {
            return argument.Direction switch
            {
                Direction.OutCalleeAllocates => "out ",
                Direction.OutCallerAllocates => "ref ",
                _ => ""
            };
        }
        
        private static string GetType(Argument argument, Target target, Namespace currentNamespace) => target switch
        {
            Target.Managed => argument.WriteManagedType(currentNamespace),
            Target.Native => argument.WriteNativeType(currentNamespace),
            _ => throw new Exception($"Unknown {nameof(Target)}")
        };

        private static string GetNullable(Argument argument)
            => argument.Nullable ? "?" : string.Empty;
    }
}
