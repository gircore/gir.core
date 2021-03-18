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
            builder.Append(argument.SymbolName);

            return builder.ToString();
        }

        private static string GetFullType(this Argument argument, Target target, Namespace currentNamespace)
        {
            var attribute = GetAttribute(argument, target);
            var direction = GetDirection(argument);
            var type = GetType(argument, target, currentNamespace);

            return $"{attribute}{direction}{type}";
        }

        private static string GetAttribute(Argument argument, Target target)
        {
            if (target == Target.Managed)
                return "";
            
            var attribute = argument.TypeInformation.Array.GetMarshallAttribute();
            
            if (attribute.Length > 0)
                attribute += " ";

            return attribute;
        }

        private static string GetDirection(Argument argument)
        {
            return argument switch
            {
                {Direction: Direction.OutCalleeAllocates} => "out ",
                {Direction: Direction.OutCallerAllocates} => "ref ",
                _ => ""
            };
        }

        private static string GetType(this Argument argument, Target target, Namespace currentNamespace) => target switch
        {
            Target.Managed => argument.WriteManagedType(currentNamespace) + GetNullable(argument),
            Target.Native => argument.WriteNativeType(currentNamespace),
            _ => throw new Exception($"Unknown {nameof(Target)}")
        };

        private static string GetNullable(Argument argument)
            => argument.Nullable ? "?" : string.Empty;
        
        internal static string WriteMarshalArgumentToManaged(this Argument arg, Namespace currentNamespace)
        {
            // TODO: We need to support disguised structs (opaque types)
            var expression = (arg.SymbolReference.GetSymbol(), arg.TypeInformation) switch
            {
                (Record r, {IsPointer: true, Array: null}) => $"Marshal.PtrToStructure<{r.SymbolName}>({arg.SymbolName});",
                (Record r, {IsPointer: true, Array:{}}) => $"{arg.SymbolName}.MarshalToStructure<{r.SymbolName}>();",
                (Class {IsFundamental: true} c, {IsPointer: true, Array: null}) => $"{c.SymbolName}.From({arg.SymbolName});",
                (Class c, {IsPointer: true, Array: null}) => $"Object.WrapHandle<{c.SymbolName}>({arg.SymbolName}, {arg.Transfer.IsOwnedRef().ToString().ToLower()});",
                (Class c, {IsPointer: true, Array: {}}) => throw new NotImplementedException($"Cant create delegate for argument {arg.SymbolName}"),
                _ => $"({arg.WriteManagedType(currentNamespace)}){arg.SymbolName};" // Other -> Try a brute-force cast
            };
            
            return $"{arg.WriteManagedType(currentNamespace)} {arg.SymbolName}Managed = " + expression;
        }
    }
}
