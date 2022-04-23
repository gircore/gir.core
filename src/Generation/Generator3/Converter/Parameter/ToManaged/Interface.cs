using System;
using GirModel;

namespace Generator3.Converter.Parameter.ToManaged;

internal class Interface : ParameterConverter
{
    public bool Supports(GirModel.AnyType type)
        => type.Is<GirModel.Interface>();

    public string? GetExpression(GirModel.Parameter parameter, out string variableName)
    {
        if (!parameter.IsPointer)
            throw new NotImplementedException($"{parameter.AnyType}: Unpointed interface parameter not yet supported");

        var iface = (GirModel.Interface) parameter.AnyType.AsT0;

        variableName = parameter.GetConvertedName();
        return $"var {variableName} = GObject.Internal.ObjectWrapper.WrapHandle<{iface.GetFullyQualified()}>({parameter.GetPublicName()}, {parameter.Transfer.IsOwnedRef().ToString().ToLower()});";
    }
}
