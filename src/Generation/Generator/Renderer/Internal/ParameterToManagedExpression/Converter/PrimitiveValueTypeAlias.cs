using System;
using System.Collections.Generic;

namespace Generator.Renderer.Internal.ParameterToManagedExpressions;

internal class PrimitiveValueTypeAlias : ToManagedParameterConverter
{
    public bool Supports(GirModel.AnyType type)
        => type.IsAlias<GirModel.PrimitiveValueType>();

    public void Initialize(ParameterToManagedData parameterData, IEnumerable<ParameterToManagedData> parameters)
    {
        if (parameterData.Parameter.Direction != GirModel.Direction.In)
            throw new NotImplementedException($"{parameterData.Parameter.AnyTypeOrVarArgs}: Primitive value type with direction != in not yet supported");

        switch (parameterData.Parameter)
        {
            case { Direction: GirModel.Direction.In, IsPointer: true }:
                InPointer(parameterData);
                break;
            case { Direction: GirModel.Direction.In, IsPointer: false }:
                In(parameterData);
                break;
            default:
                throw new Exception($"Can not convert primitive value type alias parameter {parameterData.Parameter} to managed. Not yet supported.");
        }
    }

    private static void In(ParameterToManagedData parameterData)
    {
        var variableName = Model.Parameter.GetName(parameterData.Parameter);

        parameterData.SetSignatureName(variableName);
        parameterData.SetCallName(variableName);
    }

    private static void InPointer(ParameterToManagedData parameterData)
    {
        var variableName = Model.Parameter.GetName(parameterData.Parameter);

        parameterData.SetSignatureName(variableName);
        parameterData.SetCallName(ParameterDirection.Ref() + variableName);
    }
}
