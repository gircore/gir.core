using System;
using System.Text;
using Repository.Model;

namespace Generator
{
    internal static class ParameterExtension
    {
        internal static string Write(this Parameter parameter, Target target,  Namespace currentNamespace, bool useSafeHandle = true)
        {
            var type = GetFullType(parameter, target, currentNamespace, useSafeHandle);

            var builder = new StringBuilder();
            builder.Append(type);
            builder.Append(' ');
            builder.Append(parameter.SymbolName);

            return builder.ToString();
        }

        private static string GetFullType(this Parameter parameter, Target target, Namespace currentNamespace, bool useSafeHandle)
        {
            var direction = GetDirection(parameter);
            var type = GetType(parameter, target, currentNamespace, useSafeHandle);

            return $"{direction}{type}";
        }

        private static string GetDirection(Parameter parameter)
        {
            return parameter switch
            {
                // Arrays are automatically marshalled correctly. They don't need any direction
                {Direction: Direction.Ref, TypeInformation: {Array: { }}} => "",

                {Direction: Direction.Ref} => "ref ",
                {Direction: Direction.Out, CallerAllocates: true} => "ref ",
                {Direction: Direction.Out} => "out ",
                _ => ""
            };
        }

        private static string GetType(this Parameter parameter, Target target, Namespace currentNamespace, bool useSafeHandle)
        {
            Symbol symbol = parameter.SymbolReference.GetSymbol();
            
            // Do nullability checks (actual logic in `TypeExtension.WriteType()`)
            return (target, parameter, symbol) switch
            {
                (Target.Managed, _, _) => Nullable(parameter, target, currentNamespace, useSafeHandle),

                //IntPtr can't be nullable they can be "nulled" via IntPtr.Zero
                (Target.Native, _, {SymbolName: {Value: "IntPtr"}}) => NotNullable(parameter, target, currentNamespace, useSafeHandle),

                //Native arrays can not be nullable
                (Target.Native, {TypeInformation: {Array: { }}}, _) => NotNullable(parameter, target, currentNamespace, useSafeHandle),

                //Classes are represented as IntPtr and should not be nullable
                (Target.Native, _, Class) => NotNullable(parameter, target, currentNamespace, useSafeHandle),

                //Records are represented as SafeHandles and are not nullable
                (Target.Native, _, Record) => NotNullable(parameter, target, currentNamespace, useSafeHandle),

                //Pointer to primitive value types are not nullable
                (Target.Native, {TypeInformation: {IsPointer: true}}, PrimitiveValueType) => NotNullable(parameter, target, currentNamespace, useSafeHandle),

                (Target.Native, _, _) => Nullable(parameter, target, currentNamespace, useSafeHandle),
                
                _ => throw new Exception($"Unknown {nameof(Target)}")
            };
        }

        private static string NotNullable(Parameter parameter, Target target, Namespace currentNamespace, bool useSafeHandle)
            => parameter.WriteType(target, currentNamespace, useSafeHandle);

        private static string Nullable(Parameter parameter, Target target, Namespace currentNamespace, bool useSafeHandle)
            => parameter.WriteType(target, currentNamespace, useSafeHandle) + GetNullable(parameter);

        private static string GetNullable(Parameter singleParameter)
            => singleParameter.Nullable ? "?" : string.Empty;

        internal static string WriteMarshalArgumentToManaged(this Parameter arg, Namespace currentNamespace)
        {
            var type = arg.WriteType(Target.Managed, currentNamespace);

            var expression = Convert.NativeToManaged(
                fromParam: arg.SymbolName.ToString(),
                symbol: arg.SymbolReference.GetSymbol(),
                typeInfo: arg.TypeInformation,
                currentNamespace: currentNamespace,
                transfer: arg.Transfer
            );

            return $"{type} {arg.SymbolName}Managed = " + expression;
        }
    }
}
