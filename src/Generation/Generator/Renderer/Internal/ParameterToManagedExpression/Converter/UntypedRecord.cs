using System;
using System.Collections.Generic;
using GirModel;

namespace Generator.Renderer.Internal.ParameterToManagedExpressions;

internal class UntypedRecord : ToManagedParameterConverter
{
    public bool Supports(GirModel.AnyType type)
        => type.Is<GirModel.Record>(out var record) && Model.Record.IsUntyped(record);

    public void Initialize(ParameterToManagedData parameterData, IEnumerable<ParameterToManagedData> parameters)
    {
        if (!parameterData.Parameter.IsPointer)
            throw new NotImplementedException($"Unpointed untyped record parameter {parameterData.Parameter.Name} ({parameterData.Parameter.AnyTypeOrVarArgs}) can not yet be converted to managed");

        switch (parameterData.Parameter.Direction)
        {
            case Direction.In:
                InRecord(parameterData);
                break;
            case Direction.Out:
                OutRecord(parameterData);
                break;
            default:
                throw new NotImplementedException($"{parameterData.Parameter.AnyTypeOrVarArgs}: untyped record with direction {parameterData.Parameter.Direction} not yet supported");
        }
    }

    private static void OutRecord(ParameterToManagedData parameterData)
    {
        switch (parameterData.Parameter.CallerAllocates)
        {
            case true:
                OutCallerAllocates(parameterData);
                break;
            case false:
                OutCalleeAllocates(parameterData);
                break;
        }
    }

    private static void OutCallerAllocates(ParameterToManagedData parameterData)
    {
        var parameterName = Model.Parameter.GetConvertedName(parameterData.Parameter);
        var nativeName = Model.Parameter.GetName(parameterData.Parameter);

        parameterData.SetSignatureName(() => parameterName);
        parameterData.SetCallName(() => "out var " + nativeName);
        parameterData.SetPostCallExpression(() => $"{nativeName}.Handle.CopyTo({parameterName});");
    }

    private static void OutCalleeAllocates(ParameterToManagedData parameterData)
    {
        var parameterName = Model.Parameter.GetConvertedName(parameterData.Parameter);
        var nativeName = Model.Parameter.GetName(parameterData.Parameter);

        parameterData.SetSignatureName(() => parameterName);
        parameterData.SetCallName(() => "out var " + nativeName);
        parameterData.SetPostCallExpression(() => $"{parameterName} = {nativeName}.Handle.UnownedCopy().DangerousGetHandle();");
    }

    private static void InRecord(ParameterToManagedData parameterData)
    {
        var record = (GirModel.Record) parameterData.Parameter.AnyTypeOrVarArgs.AsT0.AsT0;
        var ownedHandle = parameterData.Parameter.Transfer == GirModel.Transfer.Full;
        var variableName = Model.Parameter.GetConvertedName(parameterData.Parameter);

        var handleClass = ownedHandle
            ? Model.UntypedRecord.GetFullyQuallifiedOwnedHandle(record)
            : Model.UntypedRecord.GetFullyQuallifiedUnownedHandle(record);

        var signatureName = Model.Parameter.GetName(parameterData.Parameter);

        parameterData.SetSignatureName(() => signatureName);
        parameterData.SetExpression(() => $"var {variableName} = new {Model.Record.GetFullyQualifiedPublicClassName(record)}(new {handleClass}({signatureName}).Copy());");
        parameterData.SetCallName(() => variableName);
    }
}
