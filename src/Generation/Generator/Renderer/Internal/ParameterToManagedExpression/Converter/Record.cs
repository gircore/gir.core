using System;
using System.Collections.Generic;

namespace Generator.Renderer.Internal.ParameterToManagedExpressions;

internal class Record : ToManagedParameterConverter
{
    public bool Supports(GirModel.AnyType type)
        => type.Is<GirModel.Record>(out var record) && Model.Record.IsStandard(record);

    public void Initialize(ParameterToManagedData parameterData, IEnumerable<ParameterToManagedData> parameters)
    {
        if (Model.Parameter.IsGLibError(parameterData.Parameter))
            ErrorRecord(parameterData);
        else
            RegularRecord(parameterData);
    }

    private static void ErrorRecord(ParameterToManagedData parameterData)
    {
        parameterData.IsGLibErrorParameter = true;

        var name = Model.Parameter.GetName(parameterData.Parameter);
        parameterData.SetSignatureName(name);
        parameterData.SetCallName(name);
    }

    private static void RegularRecord(ParameterToManagedData parameterData)
    {
        if (parameterData.Parameter.Direction != GirModel.Direction.In)
            throw new NotImplementedException($"{parameterData.Parameter.AnyTypeOrVarArgs}: record with direction != in not yet supported");

        if (!parameterData.Parameter.IsPointer)
            throw new NotImplementedException($"Unpointed record parameter {parameterData.Parameter.Name} ({parameterData.Parameter.AnyTypeOrVarArgs}) can not yet be converted to managed");

        var record = (GirModel.Record) parameterData.Parameter.AnyTypeOrVarArgs.AsT0.AsT0;
        var ownedHandle = parameterData.Parameter.Transfer == GirModel.Transfer.Full;
        var variableName = Model.Parameter.GetConvertedName(parameterData.Parameter);

        var handleClass = ownedHandle
            ? Model.Record.GetFullyQualifiedInternalOwnedHandle(record)
            : Model.Record.GetFullyQualifiedInternalUnownedHandle(record);

        var signatureName = Model.Parameter.GetName(parameterData.Parameter);

        parameterData.SetSignatureName(signatureName);
        parameterData.SetExpression($"var {variableName} = new {Model.Record.GetFullyQualifiedPublicClassName(record)}(new {handleClass}({signatureName}));");
        parameterData.SetCallName(variableName);
    }
}
