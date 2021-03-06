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

        internal static string WriteTypeAndName(this Argument argument, Target target, Namespace currentNamespace)
        {
            return GetType(argument, target, currentNamespace) + " " + argument.NativeName;
        }
        
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

            return $"{attribute}{direction}{type}";
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
        
        internal static string GetType(this Argument argument, Target target, Namespace currentNamespace) => target switch
        {
            Target.Managed => argument.WriteManagedType(currentNamespace) + GetNullable(argument),
            Target.Native => argument.WriteNativeType(currentNamespace) + GetNullable(argument),
            _ => throw new Exception($"Unknown {nameof(Target)}")
        };

        private static string GetNullable(Argument argument)
            => argument.Nullable ? "?" : string.Empty;
    }
}
