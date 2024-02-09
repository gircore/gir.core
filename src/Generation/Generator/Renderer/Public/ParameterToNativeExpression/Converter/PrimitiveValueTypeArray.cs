using System;
using System.Collections.Generic;
using System.Linq;

namespace Generator.Renderer.Public.ParameterToNativeExpressions;

internal class PrimitiveValueTypeArray : ToNativeParameterConverter
{
    public bool Supports(GirModel.AnyType type)
        => type.IsArray<GirModel.PrimitiveValueType>();

    public void Initialize(ParameterToNativeData parameter, IEnumerable<ParameterToNativeData> allParameters)
    {
        switch (parameter.Parameter)
        {
            case { Direction: GirModel.Direction.Out, CallerAllocates: true }
                when parameter.Parameter.AnyTypeOrVarArgs.AsT0.AsT1.Length is not null:
            case { Direction: GirModel.Direction.In }
                when parameter.Parameter.AnyTypeOrVarArgs.AsT0.AsT1.Length is not null:
            case { Direction: GirModel.Direction.InOut }
                when parameter.Parameter.AnyTypeOrVarArgs.AsT0.AsT1.Length is not null:
                Span(parameter, allParameters);
                break;
            case { Direction: GirModel.Direction.In }:
                Ref(parameter);
                break;
            default:
                throw new Exception($"{parameter.Parameter}: This kind of primitive array is not yet supported");
        }
    }

    private static void Ref(ParameterToNativeData parameter)
    {
        var parameterName = Model.Parameter.GetName(parameter.Parameter);
        parameter.SetSignatureName(() => parameterName);
        parameter.SetCallName(() => $"ref {parameterName}");

        //TODO
        throw new Exception("Test missing");
    }

    private static void Span(ParameterToNativeData parameter, IEnumerable<ParameterToNativeData> allParameters)
    {
        var parameterName = Model.Parameter.GetName(parameter.Parameter);
        parameter.SetSignatureName(() => parameterName);
        parameter.SetCallName(() => $"ref MemoryMarshal.GetReference({parameterName})");

        var lengthIndex = parameter.Parameter.AnyTypeOrVarArgs.AsT0.AsT1.Length ?? throw new Exception("Length missing");
        var lengthParameter = allParameters.ElementAt(lengthIndex);
        var type = Model.Type.GetName(lengthParameter.Parameter.AnyTypeOrVarArgs.AsT0.AsT0);

        switch (lengthParameter.Parameter.Direction)
        {
            case GirModel.Direction.In:
                lengthParameter.IsArrayLengthParameter = true;
                lengthParameter.SetCallName(() => $"({type}) {parameterName}.Length");
                break;
            case GirModel.Direction.InOut:
                lengthParameter.IsInOutArrayLengthParameter = true;
                lengthParameter.SetExpression(() => $"{Model.Parameter.GetName(lengthParameter.Parameter)} = ({type}) {parameterName}.Length;");
                break;
            default:
                throw new Exception("Unknown direction for length parameter");
        }
    }
}
