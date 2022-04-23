using System;
using GirModel;

namespace Generator3.Converter.Parameter.ToNative;

internal class Record : ParameterConverter
{
    public bool Supports(AnyType type)
        => type.Is<GirModel.Record>();

    public string? GetExpression(GirModel.Parameter parameter, out string variableName)
    {
        if (parameter.Direction != Direction.In)
            throw new NotImplementedException($"{parameter.AnyType}: record parameter with direction != in not yet supported");

        if (!parameter.IsPointer)
            throw new NotImplementedException($"{parameter.AnyType}: Not pointed record types can not yet be converted to native");

        if (parameter.Nullable)
        {
            var record = (GirModel.Record) parameter.AnyType.AsT0;
            variableName = parameter.GetPublicName() + "?.Handle ?? " + record.GetFullyQualifiedInternalNullHandleInstance();
        }
        else
        {
            variableName = parameter.GetPublicName() + ".Handle";
        }

        return null;
    }
}
