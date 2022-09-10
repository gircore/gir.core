using System.Collections.Generic;
using System.Linq;
using System.Text;
using GirModel;
using Parameter = Generator.Model.Parameter;

namespace Generator.Renderer.Public;

internal static class CallbackCommonHandlerRenderUtils
{
    public static string RenderNativeCallback(GirModel.Callback callback, Scope? scope)
    {
        string? nativeCallback;

        //TODO: try / catch is a helper as long as there are errors
        try
        {
            nativeCallback = $@"
NativeCallback = ({callback.Parameters.Select(Parameter.GetName).Join(", ")}) => {{
    {RenderConvertParameterStatements(callback, out IEnumerable<string> parameters)}
    {RenderCallStatement(callback, parameters, out var resultVariableName)}
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

    private static string RenderConvertParameterStatements(GirModel.Callback callback, out IEnumerable<string> parameters)
    {
        var call = new StringBuilder();
        var names = new List<string>();

        foreach (GirModel.Parameter p in callback.Parameters.Where(x => x.Closure is null or 0))
        {
            call.AppendLine(p.ToManaged(out var variableName));
            names.Add(variableName);
        }

        parameters = names;

        return call.ToString();
    }

    private static string RenderCallStatement(GirModel.Callback callback, IEnumerable<string> parameterNames, out string resultVariableName)
    {
        resultVariableName = "managedCallbackResult";
        var call = $"managedCallback({parameterNames.Join(", ")});";

        if (callback.ReturnType.AnyType.Is<GirModel.Void>())
            return call;

        return $"var {resultVariableName} = " + call;
    }

    private static string RenderFreeStatement(Scope? scope)
    {
        return scope == Scope.Async
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
