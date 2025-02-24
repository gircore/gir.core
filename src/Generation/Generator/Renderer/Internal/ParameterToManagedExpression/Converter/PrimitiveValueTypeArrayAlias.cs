using System;
using System.Collections.Generic;

namespace Generator.Renderer.Internal.ParameterToManagedExpressions;

internal class PrimitiveValueTypeArrayAlias : ToManagedParameterConverter
{
    public bool Supports(GirModel.AnyType type)
        => type.IsArrayAlias<GirModel.PrimitiveValueType>();

    public void Initialize(ParameterToManagedData parameterData, IEnumerable<ParameterToManagedData> parameters)
    {
        if (parameterData.Parameter.Direction != GirModel.Direction.In)
            throw new NotImplementedException($"{parameterData.Parameter.AnyTypeOrVarArgs}: Primitive value type with direction != in not yet supported");

        var variableName = Model.Parameter.GetName(parameterData.Parameter);
        var callName = variableName + "Managed";

        var type = (GirModel.Alias) parameterData.Parameter.AnyTypeOrVarArgs.AsT0.AsT1.AnyType.AsT0;
        var ns = Model.Namespace.GetPublicName(type.Namespace);
        var typeName = Model.Type.GetName(type);

        parameterData.SetSignatureName(() => variableName);
        parameterData.SetExpression(() => $"var {callName} = {variableName}.Select(x => new {ns}.{typeName}(x)).ToArray();");
        parameterData.SetCallName(() => callName);
    }
}
