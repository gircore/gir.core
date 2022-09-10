using System;
using Generator.Model;

namespace Generator.Renderer.Public.ParameterExpressions.ToManaged;

internal class Interface : ToManagedParameterConverter
{
    public bool Supports(GirModel.AnyType type)
        => type.Is<GirModel.Interface>();

    public string? GetExpression(GirModel.Parameter parameter, out string variableName)
    {
        if (!parameter.IsPointer)
            throw new NotImplementedException($"{parameter.AnyType}: Unpointed interface parameter not yet supported");

        var iface = (GirModel.Interface) parameter.AnyType.AsT0;

        variableName = Parameter.GetConvertedName(parameter);
        return $"var {variableName} = GObject.Internal.ObjectWrapper.WrapHandle<{ComplexType.GetFullyQualified(iface)}>({Parameter.GetName(parameter)}, {Transfer.IsOwnedRef(parameter.Transfer).ToString().ToLower()});";
    }
}
