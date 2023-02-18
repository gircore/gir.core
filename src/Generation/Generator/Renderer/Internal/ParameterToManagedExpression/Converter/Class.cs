using System;
using Generator.Model;

namespace Generator.Renderer.Internal.ParameterToManagedExpressions;

internal class Class : ToManagedParameterConverter
{
    public bool Supports(GirModel.AnyType type)
        => type.Is<GirModel.Class>();

    public string? GetExpression(GirModel.Parameter parameter, out string variableName)
    {
        if (!parameter.IsPointer)
            throw new NotImplementedException($"{parameter.AnyTypeOrVarArgs}: Unpointed class parameter not yet supported");

        var cls = (GirModel.Class) parameter.AnyTypeOrVarArgs.AsT0.AsT0;

        if (cls.Fundamental)
        {
            variableName = Parameter.GetConvertedName(parameter);
            return $"var {variableName} = new {ComplexType.GetFullyQualified(cls)}({Parameter.GetName(parameter)});";
        }
        else
        {
            variableName = Parameter.GetConvertedName(parameter);
            return $"var {variableName} = GObject.Internal.ObjectWrapper.WrapHandle<{ComplexType.GetFullyQualified(cls)}>({Parameter.GetName(parameter)}, {Transfer.IsOwnedRef(parameter.Transfer).ToString().ToLower()});";
        }
    }
}
