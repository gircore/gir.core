using System;
using System.Collections.Generic;
using System.Linq;

namespace Generator.Renderer.Internal.ParameterToManagedExpressions;

internal class PrimitiveValueTypeArray : ToManagedParameterConverter
{
    public bool Supports(GirModel.AnyType type)
        => type.IsArray<GirModel.PrimitiveValueType>();

    public void Initialize(ParameterToManagedData parameter, IEnumerable<ParameterToManagedData> parameters)
    {
        if (parameter.Parameter is { Direction: GirModel.Direction.In })
        {
            if (parameter.Parameter.AnyTypeOrVarArgs.AsT0.AsT1.Length is not null)
                Span(parameter, parameters);
            else
                Ref(parameter);
        }
        else
            throw new Exception($"{parameter.Parameter}: This kind of internal byte array is not yet supported");
    }

    private static void Ref(ParameterToManagedData parameter)
    {
        var signatureName = Model.Parameter.GetName(parameter.Parameter);
        parameter.SetSignatureName(signatureName);
        parameter.SetCallName($"ref {signatureName}");
    }

    private static void Span(ParameterToManagedData parameter, IEnumerable<ParameterToManagedData> allParameters)
    {
        var lengthIndex = parameter.Parameter.AnyTypeOrVarArgs.AsT0.AsT1.Length ?? throw new Exception("Length missing");
        var lengthParameter = allParameters.ElementAt(lengthIndex);
        var lengthSignatureName = Model.Parameter.GetName(lengthParameter.Parameter);
        lengthParameter.IsArrayLengthParameter = true;
        lengthParameter.SetSignatureName(lengthSignatureName);

        var signatureName = Model.Parameter.GetName(parameter.Parameter);
        var callName = signatureName + "Span";

        parameter.SetSignatureName(signatureName);
        parameter.SetExpression($"var {callName} = MemoryMarshal.CreateSpan(ref {signatureName}, (int) {lengthSignatureName});");
        parameter.SetCallName(callName);
    }
}
