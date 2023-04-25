using System;
using System.Collections.Generic;

namespace Generator.Renderer.Internal.ParameterToManagedExpressions;

internal class Class : ToManagedParameterConverter
{
    public bool Supports(GirModel.AnyType type)
        => type.Is<GirModel.Class>();

    public void Initialize(ParameterToManagedData parameterData, IEnumerable<ParameterToManagedData> parameters)
    {
        if (!parameterData.Parameter.IsPointer)
            throw new NotImplementedException($"{parameterData.Parameter.AnyTypeOrVarArgs}: Unpointed class parameter not yet supported");

        var cls = (GirModel.Class) parameterData.Parameter.AnyTypeOrVarArgs.AsT0.AsT0;

        if (cls.Fundamental)
        {
            var paramterName = Model.Parameter.GetName(parameterData.Parameter);
            var variableName = Model.Parameter.GetConvertedName(parameterData.Parameter);

            parameterData.SetSignatureName(paramterName);
            parameterData.SetExpression($"var {variableName} = new {Model.ComplexType.GetFullyQualified(cls)}({paramterName});");
            parameterData.SetCallName(variableName);
        }
        else
        {
            var parameterName = Model.Parameter.GetName(parameterData.Parameter);
            var variableName = Model.Parameter.GetConvertedName(parameterData.Parameter);

            parameterData.SetSignatureName(parameterName);
            parameterData.SetExpression($"var {variableName} = GObject.Internal.ObjectWrapper.WrapHandle<{Model.ComplexType.GetFullyQualified(cls)}>({parameterName}, {Model.Transfer.IsOwnedRef(parameterData.Parameter.Transfer).ToString().ToLower()});");
            parameterData.SetCallName(variableName);
        }
    }
}
