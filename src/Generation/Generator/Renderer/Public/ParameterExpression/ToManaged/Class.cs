using System;
using Generator.Model;

namespace Generator.Renderer.Public.ParameterExpressions.ToManaged;

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
