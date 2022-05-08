using System;
using Generator.Model;

namespace Generator.Renderer.Public.ParameterExpressions.ToNative;

internal class Record : ParameterConverter
{
    public bool Supports(GirModel.AnyType type)
        => type.Is<GirModel.Record>();

    public string? GetExpression(GirModel.Parameter parameter, out string variableName)
    {
        if (parameter.Direction != GirModel.Direction.In)
            throw new NotImplementedException($"{parameter.AnyType}: record parameter with direction != in not yet supported");

        if (!parameter.IsPointer)
            throw new NotImplementedException($"{parameter.AnyType}: Not pointed record types can not yet be converted to native");

        if (parameter.Nullable)
        {
            var record = (GirModel.Record) parameter.AnyType.AsT0;
            variableName = Parameter.GetName(parameter) + "?.Handle ?? " + Model.Record.GetFullyQualifiedInternalNullHandleInstance(record);
        }
        else
        {
            variableName = Parameter.GetName(parameter) + ".Handle";
        }

        return null;
    }
}
