using System;
using System.Collections.Generic;
using System.Linq;

namespace Generator.Renderer.Internal.ParameterToManagedExpressions;

internal class UntypedRecordArray : ToManagedParameterConverter
{
    public bool Supports(GirModel.AnyType type)
        => type.IsArray<GirModel.Record>(out var record) && Model.Record.IsUntyped(record);

    public void Initialize(ParameterToManagedData parameterData, IEnumerable<ParameterToManagedData> parameters)
    {
        switch (parameterData.Parameter)
        {
            case { Direction: GirModel.Direction.In }
                when parameterData.Parameter.AnyTypeOrVarArgs.AsT0.AsT1.Length is not null:
                WithLength(parameterData, parameters);
                break;
            case { Direction: GirModel.Direction.In }:
                WithoutLength(parameterData);
                break;
            default:
                throw new Exception($"{parameterData.Parameter}: This kind of typed record array is not yet supported");
        }
    }

    private static void WithoutLength(ParameterToManagedData parameter)
    {
        var parameterName = Model.Parameter.GetName(parameter.Parameter);
        parameter.SetSignatureName(() => parameterName);
        parameter.SetCallName(() => $"ref {parameterName}");

        //TODO
        throw new Exception("Test missing for untyped record array passed in via a ref to managed");
    }

    private static void WithLength(ParameterToManagedData parameter, IEnumerable<ParameterToManagedData> allParameters)
    {
        if (parameter.Parameter.AnyTypeOrVarArgs.AsT0.AsT1.IsPointer)
            PointerArrayWithLength(parameter, allParameters);
        else
            StructArrayWithLength(parameter, allParameters);
    }

    private static void PointerArrayWithLength(ParameterToManagedData parameter, IEnumerable<ParameterToManagedData> allParameters)
    {
        throw new Exception("Pointer array not yet supported for untyped record arrays");
    }

    private static void StructArrayWithLength(ParameterToManagedData parameter, IEnumerable<ParameterToManagedData> allParameters)
    {
        if (parameter.Parameter.Transfer is GirModel.Transfer.Container or GirModel.Transfer.Full)
            throw new Exception("Can't transfer ownership to native code for untyped record");

        var record = (GirModel.Record) parameter.Parameter.AnyTypeOrVarArgs.AsT0.AsT1.AnyType.AsT0;
        var parameterName = Model.Parameter.GetName(parameter.Parameter);
        var nativeVariableName = parameterName + "Native";

        var lengthIndex = parameter.Parameter.AnyTypeOrVarArgs.AsT0.AsT1.Length ?? throw new Exception("Length missing");
        var lengthParameter = allParameters.ElementAt(lengthIndex);
        lengthParameter.IsArrayLengthParameter = true;

        var method = parameter.Parameter.Nullable
            ? "ToNullableArray"
            : "ToArray";

        parameter.SetSignatureName(() => parameterName);
        parameter.SetCallName(() => $"{nativeVariableName}.{method}((int){lengthParameter.GetCallName()})");

        var nullableExpression = parameter.Parameter.Nullable
            ? $"{parameterName} == System.IntPtr.Zero ? {Model.UntypedRecord.GetFullyQuallifiedArrayNullHandle(record)} : "
            : string.Empty;

        parameter.SetExpression(() => $"var {nativeVariableName} = {nullableExpression} new {Model.UntypedRecord.GetFullyQuallifiedArrayUnownedHandle(record)}({parameterName}, (int) {lengthParameter.GetCallName()});");
    }
}
