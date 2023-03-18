using System;

namespace Generator.Renderer.Internal.ParameterToManagedExpressions;

internal class PrimitiveValueType : ToManagedParameterConverter
{
    public bool Supports(GirModel.AnyType type)
        => type.Is<GirModel.PrimitiveValueType>();

    public string? GetExpression(GirModel.Parameter parameter, out string variableName)
    {
        if (parameter.IsPointer)
            throw new NotImplementedException($"{parameter.AnyTypeOrVarArgs}: Pointed primitive value types can not yet be converted to managed");

        if (parameter.Direction != GirModel.Direction.In)
            throw new NotImplementedException($"{parameter.AnyTypeOrVarArgs}: Primitive value type with direction != in not yet supported");

        //We don't need any conversion for native parameters
        variableName = Model.Parameter.GetName(parameter);
        return null;
    }
}
