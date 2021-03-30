using System;
using System.Text;
using Repository.Model;

namespace Generator
{
    internal static class ParameterExtension
    {
        internal static string Write(this Parameter parameter, Target target,  Namespace currentNamespace)
        {
            var type = GetFullType(parameter, target, currentNamespace);
            
            var builder = new StringBuilder();
            builder.Append(type);
            builder.Append(' ');
            builder.Append(parameter.SymbolName);

            return builder.ToString();
        }

        private static string GetFullType(this Parameter parameter, Target target, Namespace currentNamespace)
        {
            var direction = GetDirection(parameter);
            var type = GetType(parameter, target, currentNamespace);

            return $"{direction}{type}";
        }

        private static string GetDirection(Parameter parameter)
        {
            return parameter switch
            {
                {Direction: Direction.Out, CallerAllocates: true} => "ref ",
                {Direction: Direction.Out} => "out ",
                _ => ""
            };
        }

        private static string GetType(this Parameter parameter, Target target, Namespace currentNamespace) => target switch
        {
            Target.Managed => parameter.WriteType(target, currentNamespace) + GetNullable(parameter),
            //IntPtr can't be nullable they can be "nulled" via IntPtr.Zero
            Target.Native when parameter.SymbolReference.GetSymbol().SymbolName == "IntPtr" => parameter.WriteType(target, currentNamespace),
            Target.Native => parameter.WriteType(target, currentNamespace) + GetNullable(parameter),
            _ => throw new Exception($"Unknown {nameof(Target)}")
        };

        private static string GetNullable(Parameter singleParameter)
            => singleParameter.Nullable ? "?" : string.Empty;
        
        internal static string WriteMarshalArgumentToManaged(this Parameter arg, Namespace currentNamespace)
        {
            var type = arg.WriteType(Target.Managed, currentNamespace);
            
            // TODO: We need to support disguised structs (opaque types)
            var expression = (arg.SymbolReference.GetSymbol(), arg.TypeInformation) switch
            {
                (Record r, {IsPointer: true, Array: null}) => $"default; //TODO Marshal.PtrToStructure<{r.SymbolName}>({arg.SymbolName});",
                (Record r, {IsPointer: true, Array:{}}) => $"default; //TODO {arg.SymbolName}.MarshalToStructure<{r.SymbolName}>();",
                (Class {IsFundamental: true} c, {IsPointer: true, Array: null}) => $"{c.SymbolName}.From({arg.SymbolName});",
                (Class c, {IsPointer: true, Array: null}) => $"GObject.Object.WrapHandle<{type}>({arg.SymbolName}, {arg.Transfer.IsOwnedRef().ToString().ToLower()});",
                (Class c, {IsPointer: true, Array: {}}) => throw new NotImplementedException($"Cant create delegate for argument {arg.SymbolName}"),
                (Interface i, {IsPointer: true, Array: null}) => $"GObject.Object.WrapHandle<{type}>({arg.SymbolName}, {arg.Transfer.IsOwnedRef().ToString().ToLower()});",
                _ => $"default; //TODO ({arg.WriteType(Target.Managed, currentNamespace)}){arg.SymbolName};" // Other -> Try a brute-force cast
            };
            
            return $"{type} {arg.SymbolName}Managed = " + expression;
        }
    }
}
