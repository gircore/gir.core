using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Generator3.Converter;
using GirModel;

namespace Generator3.Generation.Callback
{
    public static class CommonHandlerRenderUtils
    {
        public static string RenderNativeCallback(CommonHandlerModel model)
        {
            string? nativeCallback;
            // Helper as long as there are errors
            try
            {
                nativeCallback = $@"
NativeCallback = ({model.InternalParameters.Select(p => p.GetPublicName()).Join(", ")}) => {{
    {RenderConvertParameterStatements(model, out IEnumerable<string> parameters)}
    {RenderCallStatement(model, parameters, out var resultVariableName)}
    {RenderReturnStatement(model, resultVariableName)}
}};";
            }
            catch (Exception ex)
            {
                Log.Warning($"Can not generate callback for {model.Name}: {ex.Message}");
                nativeCallback = string.Empty;
            }

            return nativeCallback;
        }

        private static string RenderCallStatement(CommonHandlerModel model, IEnumerable<string> parameterNames, out string resultVariableName)
        {
            resultVariableName = "managedCallbackResult";
            var call = $"managedCallback({parameterNames.Join(", ")});";

            if (model.InternalReturnType.Model.AnyType.Is<GirModel.Void>())
                return call;

            return $"var {resultVariableName} = " + call;
        }

        private static string RenderConvertParameterStatements(CommonHandlerModel model, out IEnumerable<string> parameters)
        {
            var call = new StringBuilder();
            var names = new List<string>();

            foreach (Parameter p in model.PublicParameters)
            {
                call.AppendLine(p.ToManaged(out var variableName));
                names.Add(variableName);
            }

            parameters = names;

            return call.ToString();
        }

        private static string RenderReturnStatement(CommonHandlerModel model, string returnVariableName)
        {
            return model.InternalReturnType.Model.AnyType.Is<GirModel.Void>()
                ? string.Empty
                : $"return {model.InternalReturnType.Model.ToNative(returnVariableName)};";
        }
    }
}
