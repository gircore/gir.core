using System;
using System.Collections.Generic;
using System.Linq;

namespace Generator.Renderer.Public.Signals;

public class InterfaceArray : SignalArgsParameterConverter
{
    public bool Supports(GirModel.AnyType type)
    {
        return type.IsArray<GirModel.Interface>();
    }

    public void Initialize(SignalArgsParameterData parameter, int index, IEnumerable<SignalArgsParameterData> parameters)
    {
        if (parameter.Parameter.AnyTypeOrVarArgs.AsT0.AsT1.Length is not null)
            LengthBased(parameter, index, parameters);
        else
            throw new NotImplementedException("Class Arrays without a length are not supported as SignalArgs");
    }

    private static void LengthBased(SignalArgsParameterData parameter, int index, IEnumerable<SignalArgsParameterData> parameters)
    {
        var arrayType = parameter.Parameter.AnyTypeOrVarArgs.AsT0.AsT1;

        if (arrayType.IsPointer)
            throw new NotImplementedException($"{parameter.Parameter.AnyTypeOrVarArgs}: Pointed class array can not yet be converted to signal args.");

        var parameterName = Model.Parameter.GetName(parameter.Parameter).ToPascalCase();
        var parameterInterface = (GirModel.Interface) arrayType.AnyType.AsT0;
        var parameterTypeName = Model.Interface.GetFullyQualifiedPublicName(parameterInterface);
        var parameterTypeFallbackName = Model.Interface.GetFullyQualifiedImplementationName(parameterInterface);
        var parameterHelperVariable = $"_{parameterName.ToCamelCase()}";

        var lengthParameter = parameters.ElementAt(arrayType.Length ?? throw new Exception("Length missing)"));
        lengthParameter.IsArrayLengthParameter = true;

        var lengthParameterTypeData = ParameterRenderer.Render(lengthParameter.Parameter);
        var getLength = $"Extract<{lengthParameterTypeData.NullableTypeName}>(Args[{arrayType.Length + 1}])";

        parameter.SetExpression(() => $"""
                                       private {parameterTypeName}[]? {parameterHelperVariable};
                                       public {parameterTypeName}[] {parameterName} => {parameterHelperVariable} ??= ExtractArray<{parameterTypeFallbackName}>(Args[{index}], {getLength});
                                       """);
    }
}
