using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Generator.Renderer.Internal.ParameterToManagedExpressions;

internal class Callback : ToManagedParameterConverter
{
    public bool Supports(GirModel.AnyType type)
        => type.Is<GirModel.Callback>();

    public void Initialize(ParameterToManagedData parameterData, IEnumerable<ParameterToManagedData> parameters)
    {
        if (parameterData.Parameter.Direction != GirModel.Direction.In)
            throw new NotImplementedException($"{parameterData.Parameter.AnyTypeOrVarArgs}: Callback with direction != in not yet supported");

        var callback = (GirModel.Callback) parameterData.Parameter.AnyTypeOrVarArgs.AsT0.AsT0;
        var ns = Model.Namespace.GetPublicName(callback.Namespace);
        var type = Model.Type.GetName(callback);
        var signatureName = Model.Parameter.GetName(parameterData.Parameter);
        var callName = signatureName + "Managed";

        var parameterToNativeDatas = Public.ParameterToNativeExpression.Initialize(callback.Parameters);

        parameterData.SetSignatureName(() => signatureName);
        parameterData.SetExpression(() => @$"var {callName} = new {ns}.{type}(({GetManagedParameters(parameterToNativeDatas)}) => 
{{ 
    {RenderContent(parameterToNativeDatas)}
    {RenderCallStatement(signatureName, callback, parameterToNativeDatas, out var resultVariableName)}
    {RenderThrowOnError(callback, parameterToNativeDatas)}
    {RenderReturnStatement(callback, resultVariableName)}
}});");
        parameterData.SetCallName(() => callName);
    }

    private static string GetManagedParameters(IEnumerable<Public.ParameterToNativeData> parameters)
    {
        var result = new List<string>();
        foreach (var parameter in parameters)
        {
            if (parameter.IsCallbackUserData)
                continue;

            if (parameter.IsDestroyNotify)
                continue;

            if (parameter.IsArrayLengthParameter)
                continue;

            if (parameter.IsGLibErrorParameter)
                continue;

            var typeData = Public.ParameterRenderer.Render(parameter.Parameter);
            result.Add($"{typeData.Direction}{typeData.NullableTypeName} {parameter.GetSignatureName()}");
        }

        return result.Join(", ");
    }

    private static string RenderContent(IEnumerable<Public.ParameterToNativeData> parameters)
    {
        return parameters
            .Select(x => x.GetExpression())
            .Where(x => !string.IsNullOrEmpty(x))
            .Cast<string>()
            .Join(Environment.NewLine);
    }

    private static string RenderCallStatement(string signatureName, GirModel.Callback callback, IReadOnlyList<Public.ParameterToNativeData> parameters, out string resultVariableName)
    {
        resultVariableName = $"result{callback.Name}";
        var call = new StringBuilder();

        if (!callback.ReturnType.AnyType.Is<GirModel.Void>())
            call.Append($"var {resultVariableName} = ");

        call.Append($"{signatureName}(");
        call.Append(string.Join(", ", parameters.Select(x => x.GetCallName())));
        call.Append(Error.RenderCallback(callback));
        call.Append(");\n");

        return call.ToString();
    }

    private static string RenderThrowOnError(GirModel.Callback callback, IEnumerable<Public.ParameterToNativeData> parameters)
    {
        return callback.Throws || parameters.Any(x => x.IsGLibErrorParameter)
            ? @"if(error != IntPtr.Zero)
    throw new GLib.GException(new GLib.Internal.ErrorUnownedHandle(error));"
            : string.Empty;
    }

    private static string RenderReturnStatement(GirModel.Callback callback, string returnVariable)
    {
        return callback.ReturnType.AnyType.Is<GirModel.Void>()
            ? string.Empty
            : $"return {Public.ReturnTypeToManagedExpression.Render(callback.ReturnType, returnVariable)};";
    }
}
