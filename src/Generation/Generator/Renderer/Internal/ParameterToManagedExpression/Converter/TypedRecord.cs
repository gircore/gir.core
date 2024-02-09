using System;
using System.Collections.Generic;
using GirModel;

namespace Generator.Renderer.Internal.ParameterToManagedExpressions;

internal class TypedRecord : ToManagedParameterConverter
{
    public bool Supports(GirModel.AnyType type)
        => type.Is<GirModel.Record>(out var record) && Model.Record.IsTyped(record);

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
        parameterData.SetSignatureName(() => name);
        parameterData.SetCallName(() => name);
    }

    private static void RegularRecord(ParameterToManagedData parameterData)
    {
        switch (parameterData.Parameter.Direction)
        {
            case GirModel.Direction.In:
                In(parameterData);
                break;
            case GirModel.Direction.Out:
                Out(parameterData);
                break;
            default:
                throw new NotImplementedException($"{parameterData.Parameter.AnyTypeOrVarArgs}: typed record with direction {parameterData.Parameter.Direction} not yet supported");
        }
    }

    private static void In(ParameterToManagedData parameterData)
    {
        var record = (GirModel.Record) parameterData.Parameter.AnyTypeOrVarArgs.AsT0.AsT0;
        var variableName = Model.Parameter.GetConvertedName(parameterData.Parameter);

        var signatureName = Model.Parameter.GetName(parameterData.Parameter);

        var ownedHandle = parameterData.Parameter switch
        {
            { Transfer: GirModel.Transfer.Full } => $"new {Model.TypedRecord.GetFullyQuallifiedOwnedHandle(record)}({signatureName})",
            { Transfer: GirModel.Transfer.None } => $"{Model.TypedRecord.GetFullyQuallifiedOwnedHandle(record)}.FromUnowned({signatureName})",
            _ => throw new Exception($"Unknown transfer type for typed record parameter {parameterData.Parameter.Name}")
        };

        var nullable = parameterData.Parameter.Nullable
            ? $" {signatureName} == IntPtr.Zero ? null :"
            : string.Empty;

        parameterData.SetSignatureName(() => signatureName);
        parameterData.SetExpression(() => $"var {variableName} ={nullable} new {Model.TypedRecord.GetFullyQualifiedPublicClassName(record)}({ownedHandle});");
        parameterData.SetCallName(() => variableName);
    }

    private static void Out(ParameterToManagedData parameterData)
    {
        var managedName = Model.Parameter.GetName(parameterData.Parameter);
        var nativeName = Model.Parameter.GetConvertedName(parameterData.Parameter);

        parameterData.SetSignatureName(() => nativeName);
        parameterData.SetCallName(() => $"out var {managedName}");

        var copy = parameterData.Parameter.Transfer switch
        {
            Transfer.None => string.Empty,
            Transfer.Full => "UnownedCopy().",
            _ => throw new NotSupportedException()
        };

        parameterData.SetPostCallExpression(() => parameterData.Parameter.Nullable
            ? $"{nativeName} = {managedName}?.Handle.{copy}DangerousGetHandle() ?? IntPtr.Zero;"
            : $"{nativeName} = {managedName}.Handle.{copy}DangerousGetHandle();");
    }
}
