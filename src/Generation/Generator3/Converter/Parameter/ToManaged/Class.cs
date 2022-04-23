using System;
using GirModel;

namespace Generator3.Converter.Parameter.ToManaged;

internal class Class : ParameterConverter
{
    public bool Supports(GirModel.AnyType type)
        => type.Is<GirModel.Class>();

    public string? GetExpression(GirModel.Parameter parameter, out string variableName)
    {
        if (!parameter.IsPointer)
            throw new NotImplementedException($"{parameter.AnyType}: Unpointed class parameter not yet supported");

        var cls = (GirModel.Class) parameter.AnyType.AsT0;

        if (cls.IsFundamental)
        {
            variableName = parameter.GetConvertedName();
            return $"var {variableName} = new {cls.GetFullyQualified()}({parameter.GetPublicName()});";
        }
        else
        {
            variableName = parameter.GetConvertedName();
            return $"var {variableName} = GObject.Internal.ObjectWrapper.WrapHandle<{cls.GetFullyQualified()}>({parameter.GetPublicName()}, {parameter.Transfer.IsOwnedRef().ToString().ToLower()});";
        }
    }
}
