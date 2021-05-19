using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Repository;
using Repository.Model;
using Type = Repository.Model.Type;

namespace Generator
{
    internal partial class DelegateGenerator
    {
        public static string WriteCallbackMarshallerParams(ParameterList parameterList)
        {
            var nativeParams = parameterList.SingleParameters.Select(x => x.SymbolName);
            return string.Join(',', nativeParams);
        }

        public static string WriteCallbackMarshallerReturn(ReturnValue returnValue, string paramName, Namespace currentNamespace)
        {
            return Convert.ManagedToNative(
                fromParam: paramName,
                typeInfo: returnValue.TypeInformation,
                currentNamespace: currentNamespace,
                transfer: returnValue.Transfer,
                type: returnValue.TypeReference.ResolvedType
                    ?? throw new ArgumentNullException($"{nameof(ReturnValue)} '{paramName}' does not have a type")
            );
        }

        public static bool CanGenerateDelegate(ParameterList parameterList, ReturnValue returnValue)
        {
            // We only support a subset of delegates at the
            // moment. Determine if we can generate based on
            // the following criteria:

            // No in/out/ref parameters
            if (parameterList.SingleParameters.Any(param => param.Direction != Direction.Default))
                return false;

            // No delegate return values
            if (returnValue.TypeReference.GetResolvedType().GetType() == typeof(Callback))
                return false;

            // No union parameters
            if (parameterList.SingleParameters.Any(param => param.TypeReference.GetResolvedType().GetType() == typeof(Union)))
                return false;

            // No union return values
            if (returnValue.TypeReference.GetResolvedType().GetType() == typeof(Union))
                return false;

            // No GObject array parameters
            if (parameterList.SingleParameters.Any(param =>
                param.TypeReference.GetResolvedType().GetType() == typeof(Class) &&
                param.TypeInformation.Array != null))
                return false;

            // No GObject array return values
            if (returnValue.TypeReference.GetResolvedType().GetType() == typeof(Class) &&
                returnValue.TypeInformation.Array != null)
                return false;

            // Go ahead and generate
            return true;
        }

        public static string WriteCallbackMarshallerBody(ParameterList parameterList, ReturnValue returnValue, Namespace currentNamespace)
        {
            if (!CanGenerateDelegate(parameterList, returnValue))
                return "throw new NotImplementedException(\"This exception is not supported\");";

            // TODO: Reuse parts of MethodGenerator for this
            // We'll need to think about freeing our unmanaged resources
            // so the block system is a good fit.

            var builder = new StringBuilder();
            var args = new List<string>();

            // Remove userData, closure data parameters
            var managedParams = parameterList.GetManagedParameters();

            foreach (Parameter arg in managedParams)
            {
                var expression = Convert.NativeToManaged(
                    fromParam: arg.SymbolName,
                    type: arg.TypeReference.GetResolvedType(),
                    typeInfo: arg.TypeInformation,
                    currentNamespace: currentNamespace,
                    transfer: arg.Transfer,
                    useSafeHandle: false
                );

                builder.AppendLine($"var {arg.SymbolName}Managed = {expression};");
                args.Add(arg.SymbolName + "Managed");
            }

            var funcArgs = string.Join(
                separator: ", ",
                values: args
            );

            var funcCall = returnValue.IsVoid()
                ? $"managedCallback({funcArgs});"
                : $"var managedCallbackResult = managedCallback({funcArgs});";

            builder.Append(funcCall);

            return builder.ToString();
        }
    }
}