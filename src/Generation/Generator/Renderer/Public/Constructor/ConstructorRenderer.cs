using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Generator.Renderer.Public;

internal static class ConstructorRenderer
{
    private static readonly List<Constructor.ConstructorConverter> Converters = new()
    {
        new Constructor.Class(),
        new Constructor.OpaqueTypedRecord(),
        new Constructor.OpaqueUntypedRecord(),
        new Constructor.TypedRecord(),
    };

    public static string Render(GirModel.Constructor constructor)
    {
        try
        {
            var constructorData = GetData(constructor);

            if (!constructorData.AllowRendering)
                return string.Empty;

            var parameters = ParameterToNativeExpression.Initialize(constructor.Parameters);

            var newKeyWord = constructorData.RequiresNewModifier
                ? "new "
                : string.Empty;

            return @$"
{VersionAttribute.Render(constructor.Version)}
public static {newKeyWord}{constructor.Parent.Name}{Nullable.Render(constructor.ReturnType)} {Model.Constructor.GetName(constructor)}({RenderParameters(parameters)})
{{
    {RenderContent(parameters)}
    {RenderCallStatement(constructor, parameters, constructorData)}
}}";
        }
        catch (Exception e)
        {
            var message = $"Did not generate constructor '{constructor.Parent.Name}.{Model.Constructor.GetName(constructor)}': {e.Message}";

            if (e is NotImplementedException)
                Log.Debug(message);
            else
                Log.Warning(message);

            return string.Empty;
        }
    }

    private static Constructor.ConstructorData GetData(GirModel.Constructor constructor)
    {
        var converter = Converters.FirstOrDefault(converter => converter.Supports(constructor));

        if (converter is null)
            throw new Exception($"No converter found to render constructor {constructor.Parent.Name}.{Model.Constructor.GetName(constructor)}");

        return converter.GetData(constructor);
    }

    private static string RenderParameters(IReadOnlyList<ParameterToNativeData> parameters)
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

            var typeData = ParameterRenderer.Render(parameter.Parameter);
            result.Add($"{typeData.Direction}{typeData.NullableTypeName} {parameter.GetSignatureName()}");
        }

        return result.Join(", ");
    }

    private static string RenderContent(IEnumerable<ParameterToNativeData> parameters)
    {
        return parameters
            .Select(x => x.GetExpression())
            .Where(x => !string.IsNullOrEmpty(x))
            .Cast<string>()
            .Join(Environment.NewLine);
    }

    private static string RenderCallStatement(GirModel.Constructor constructor, IEnumerable<ParameterToNativeData> parameters, Constructor.ConstructorData data)
    {
        var variableName = "handle";
        var call = new StringBuilder();
        call.Append($"var {variableName} = Internal.{constructor.Parent.Name}.{Model.Constructor.GetName(constructor)}(");
        call.Append(string.Join(", ", parameters.Select(x => x.GetCallName())));
        call.Append(Error.RenderParameter(constructor));
        call.Append(");" + Environment.NewLine);

        call.Append(Error.RenderThrowOnError(constructor));

        var postCallExpressions = parameters.Select(x => x.GetPostCallExpression())
            .Where(x => !string.IsNullOrEmpty(x))
            .Cast<string>();

        call.AppendJoin(Environment.NewLine, postCallExpressions);
        call.AppendLine($"return {data.GetCreateExpression(constructor, variableName)};");

        return call.ToString();
    }
}
