using System;
using System.Collections.Generic;
using Generator.Model;
using GirModel;

namespace Generator.Renderer.Public.ParameterToNativeExpressions;

internal class UntypedRecord : ToNativeParameterConverter
{
    public bool Supports(GirModel.AnyType type)
        => type.Is<GirModel.Record>(out var record) && Model.Record.IsUntyped(record);

    public void Initialize(ParameterToNativeData parameter, IEnumerable<ParameterToNativeData> _)
    {
        switch (parameter.Parameter.Direction)
        {
            case Direction.In:
                In(parameter);
                break;
            case Direction.Out:
                Out(parameter);
                break;
            default:
                throw new NotImplementedException($"{parameter.Parameter.AnyTypeOrVarArgs}: untyped record parameter '{parameter.Parameter.Name}' with direction {parameter.Parameter.Direction} not yet supported");
        }
    }

    private static void In(ParameterToNativeData parameter)
    {
        var record = (GirModel.Record) parameter.Parameter.AnyTypeOrVarArgs.AsT0.AsT0;
        var typeHandle = Model.UntypedRecord.GetFullyQuallifiedHandle(record);
        var nullHandle = Model.UntypedRecord.GetFullyQuallifiedNullHandle(record);
        var signatureName = Model.Parameter.GetName(parameter.Parameter);

        var callName = parameter.Parameter switch
        {
            { Nullable: true, Transfer: GirModel.Transfer.None } => $"({typeHandle}?) {signatureName}?.Handle ?? {nullHandle}",
            { Nullable: false, Transfer: GirModel.Transfer.None } => $"{signatureName}.Handle",
            { Transfer: GirModel.Transfer.Full } => throw new Exception("Ownership transfer not supported for untyped records"),
            _ => throw new Exception($"Can't detect call name for untyped parameter record parameter {parameter.Parameter.Name}")
        };

        parameter.SetSignatureName(() => signatureName);
        parameter.SetCallName(() => callName);
    }

    private static void Out(ParameterToNativeData parameter)
    {
        var record = (GirModel.Record) parameter.Parameter.AnyTypeOrVarArgs.AsT0.AsT0;
        var signatureName = Model.Parameter.GetName(parameter.Parameter);

        var callName = parameter.Parameter switch
        {
            { Nullable: false, Transfer: GirModel.Transfer.None, CallerAllocates: true } => Model.Parameter.GetConvertedName(parameter.Parameter),
            _ => throw new Exception($"Can't detect out call name for untyped parameter record parameter {parameter.Parameter.Name}")
        };

        parameter.SetSignatureName(() => signatureName);
        parameter.SetExpression(() => @$"var {callName} = {Model.UntypedRecord.GetFullyQuallifiedOwnedHandle(record)}.Create();
{signatureName} = new {Model.UntypedRecord.GetFullyQualifiedPublicClassName(record)}({callName});");
        parameter.SetCallName(() => callName);
    }
}
