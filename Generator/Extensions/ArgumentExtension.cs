using System;
using System.Text;
using Repository.Model;
using Type = Repository.Model.Type;

namespace Generator
{
    internal static class ArgumentExtension
    {
        internal static string Write(this Argument argument, Target target,  Namespace currentNamespace)
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
            var symbol = argument.SymbolReference.GetSymbol();
            return (argument, symbol) switch
            {
                ({Direction: Direction.OutCalleeAllocates}, _) => "out ",
                ({Direction: Direction.OutCallerAllocates}, _) => "ref ",
                ({Transfer: Transfer.None, TypeInformation: {IsPointer: true, IsConst: false}}, Record) => "out ",
                _ => ""
            };
        }

        private static string GetType(this Argument argument, Target target, Namespace currentNamespace) => target switch
        {
            Target.Managed => argument.WriteType(target, currentNamespace) + GetNullable(argument),
            Target.Native => argument.WriteType(target, currentNamespace),
            _ => throw new Exception($"Unknown {nameof(Target)}")
        };

        private static string GetNullable(Argument argument)
            => argument.Nullable ? "?" : string.Empty;
        
        internal static string WriteMarshalArgumentToManaged(this Argument arg, Namespace currentNamespace)
        {
            // TODO: We need to support disguised structs (opaque types)
            var expression = (arg.SymbolReference.GetSymbol(), arg.TypeInformation) switch
            {
                (Record r, {IsPointer: true, Array: null}) => $"default; //TODO Marshal.PtrToStructure<{r.SymbolName}>({arg.SymbolName});",
                (Record r, {IsPointer: true, Array:{}}) => $"default; //TODO {arg.SymbolName}.MarshalToStructure<{r.SymbolName}>();",
                (Class {IsFundamental: true} c, {IsPointer: true, Array: null}) => $"{c.SymbolName}.From({arg.SymbolName});",
                (Class c, {IsPointer: true, Array: null}) => $"Object.WrapHandle<{c.SymbolName}>({arg.SymbolName}, {arg.Transfer.IsOwnedRef().ToString().ToLower()});",
                (Class c, {IsPointer: true, Array: {}}) => throw new NotImplementedException($"Cant create delegate for argument {arg.SymbolName}"),
                _ => $"({arg.WriteType(Target.Managed, currentNamespace)}){arg.SymbolName};" // Other -> Try a brute-force cast
            };
            
            return $"{arg.WriteType(Target.Managed, currentNamespace)} {arg.SymbolName}Managed = " + expression;
        }
    }
}
