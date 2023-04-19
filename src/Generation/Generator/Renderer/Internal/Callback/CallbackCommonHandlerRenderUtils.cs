using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Generator.Renderer.Internal;

internal static class CallbackCommonHandlerRenderUtils
{
    public static string RenderNativeCallback(GirModel.Callback callback, GirModel.Scope? scope)
    {
        string? nativeCallback;

        //TODO: try / catch is a helper as long as there are errors
        try
        {
            var parameterData = ParameterToManagedExpression.Initialize(callback.Parameters);

            nativeCallback = $@"
NativeCallback = ({parameterData.Select(x => x.GetSignatureName()).Join(", ")}) => {{
    {RenderConvertParameterStatements(parameterData)}
    {RenderCallStatement(callback, parameterData, out var resultVariableName)}
    {RenderFreeStatement(scope)}
    {RenderReturnStatement(callback, resultVariableName)}
}};";
        }
        catch (System.Exception ex)
        {
            Log.Warning($"Can not generate callback for {callback.Name}: {ex.Message}");
            nativeCallback = string.Empty;
        }

        return nativeCallback;

    }

    private static string RenderConvertParameterStatements(IEnumerable<ParameterToManagedData> data)
    {
        var call = new StringBuilder();

        foreach (var p in data)
        {
            if (p.IsCallbackDestroyNotify)
                continue;

            if (p.IsCallbackUserData)
                continue;

            if (p.IsArrayLengthParameter)
                continue;

            call.AppendLine(p.GetExpression());
        }

        return call.ToString();
    }

    private static string RenderCallStatement(GirModel.Callback callback, IEnumerable<ParameterToManagedData> parameterData, out string resultVariableName)
    {
        resultVariableName = "managedCallbackResult";


        var parameters = new List<string>();
        foreach (var p in parameterData)
        {
            if (p.IsArrayLengthParameter)
                continue;

            if (p.IsCallbackUserData)
                continue;

            if (p.IsCallbackDestroyNotify)
                continue;

            parameters.Add(p.GetCallName());
        }

        var call = $"managedCallback({parameters.Join(", ")});";

        if (callback.ReturnType.AnyType.Is<GirModel.Void>())
            return call;

        return $"var {resultVariableName} = " + call;
    }

    private static string RenderFreeStatement(GirModel.Scope? scope)
    {
        return scope == GirModel.Scope.Async
            ? "gch.Free();"
            : string.Empty;
    }

    private static string RenderReturnStatement(GirModel.Callback callback, string returnVariableName)
    {
        return callback.ReturnType.AnyType.Is<GirModel.Void>()
            ? string.Empty
            : $"return {ReturnTypeToNativeExpression.Render(callback.ReturnType, returnVariableName)};";
    }
}
