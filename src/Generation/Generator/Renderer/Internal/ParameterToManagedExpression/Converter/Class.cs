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
            Fundamental(parameterData);
        else
            Default(parameterData);
    }

    private static void Fundamental(ParameterToManagedData parameterData)
    {
        var cls = (GirModel.Class) parameterData.Parameter.AnyTypeOrVarArgs.AsT0.AsT0;
        var paramterName = Model.Parameter.GetName(parameterData.Parameter);
        var variableName = Model.Parameter.GetConvertedName(parameterData.Parameter);

        parameterData.SetSignatureName(() => paramterName);
        parameterData.SetExpression(() => $"var {variableName} = new {Model.ComplexType.GetFullyQualified(cls)}({paramterName});");
        parameterData.SetCallName(() => variableName);
    }

    private static void Default(ParameterToManagedData parameterData)
    {
        var cls = (GirModel.Class) parameterData.Parameter.AnyTypeOrVarArgs.AsT0.AsT0;
        var parameterName = Model.Parameter.GetName(parameterData.Parameter);
        var callName = Model.Parameter.GetConvertedName(parameterData.Parameter);

        var type = Model.ComplexType.GetFullyQualified(cls);

        var wrapHandle = parameterData.Parameter.Nullable
            ? $"({type}?) GObject.Internal.InstanceWrapper.WrapNullableHandle"
            : $"({type}) GObject.Internal.InstanceWrapper.WrapHandle";

        parameterData.SetSignatureName(() => parameterName);
        parameterData.SetExpression(() => $"var {callName} = {wrapHandle}<{type}>({parameterName}, {Model.Transfer.IsOwnedRef(parameterData.Parameter.Transfer).ToString().ToLower()});");
        parameterData.SetCallName(() => callName);
    }
}
