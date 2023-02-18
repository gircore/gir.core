using System;
using Generator.Model;

namespace Generator.Renderer.Internal.ParameterToManagedExpressions;

internal class Interface : ToManagedParameterConverter
{
    public bool Supports(GirModel.AnyType type)
        => type.Is<GirModel.Interface>();

    public string? GetExpression(GirModel.Parameter parameter, out string variableName)
    {
        if (!parameter.IsPointer)
            throw new NotImplementedException($"{parameter.AnyTypeOrVarArgs}: Unpointed interface parameter not yet supported");

        var iface = (GirModel.Interface) parameter.AnyTypeOrVarArgs.AsT0.AsT0;

        variableName = Parameter.GetConvertedName(parameter);
        return $"var {variableName} = GObject.Internal.ObjectWrapper.WrapInterfaceHandle<{Model.Interface.GetFullyQualifiedImplementationName(iface)}>({Parameter.GetName(parameter)}, {Transfer.IsOwnedRef(parameter.Transfer).ToString().ToLower()});";
    }
}
