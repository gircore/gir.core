using System;
using System.Collections.Generic;

namespace Generator.Renderer.Internal.ParameterToManagedExpressions;

internal class UntypedRecord : ToManagedParameterConverter
{
    public bool Supports(GirModel.AnyTypeReference anyTypeReference)
        => anyTypeReference.References<GirModel.Record>(out var record) && Model.Record.IsUntyped(record);

    public void Initialize(ParameterToManagedData parameterData, IEnumerable<ParameterToManagedData> parameters)
    {
        if (!parameterData.Parameter.IsPointer)
            throw new NotImplementedException($"Unpointed untyped record parameter {parameterData.Parameter.Name} ({parameterData.Parameter.AnyTypeReferenceOrVarArgs}) can not yet be converted to managed");

        switch (parameterData.Parameter.Direction)
        {
            case GirModel.Direction.In:
                InRecord(parameterData);
                break;
            case GirModel.Direction.Out:
                OutRecord(parameterData);
                break;
            case GirModel.Direction.InOut:
                InOutRecord(parameterData);
                break;
            default:
                throw new NotImplementedException($"{parameterData.Parameter.AnyTypeReferenceOrVarArgs}: untyped record with direction {parameterData.Parameter.Direction} not yet supported");
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
        var record = (GirModel.Record) parameterData.Parameter.AnyTypeReferenceOrVarArgs.AsT0.AsT0.Type;
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

    private static void InOutRecord(ParameterToManagedData parameterData)
    {
        var managedName = Model.Parameter.GetName(parameterData.Parameter);
        var nativeName = Model.Parameter.GetConvertedName(parameterData.Parameter);

        parameterData.SetSignatureName(() => nativeName);
        parameterData.SetCallName(() => $"ref {managedName}");

        var copy = parameterData.Parameter.Transfer switch
        {
            GirModel.Transfer.Full => "UnownedCopy().", //Transfer full "in" and "out" direction
            _ => throw new NotSupportedException()
        };

        parameterData.SetPostCallExpression(() => parameterData.Parameter.Nullable
            ? $"{nativeName} = {managedName}?.Handle.{copy}DangerousGetHandle() ?? IntPtr.Zero;"
            : $"{nativeName} = {managedName}.Handle.{copy}DangerousGetHandle();");

        var record = (GirModel.Record) parameterData.Parameter.AnyTypeReferenceOrVarArgs.AsT0.AsT0.Type;
        var ownedHandle = parameterData.Parameter switch
        {
            { Transfer: GirModel.Transfer.Full } => $"new {Model.UntypedRecord.GetFullyQuallifiedOwnedHandle(record)}({nativeName})",
            _ => throw new Exception($"Unknown transfer type for untyped record parameter {parameterData.Parameter.Name}")
        };
        var nullable = parameterData.Parameter.Nullable
            ? $" {nativeName} == IntPtr.Zero ? null :"
            : string.Empty;
        parameterData.SetExpression(() => $"var {managedName} ={nullable} new {Model.UntypedRecord.GetFullyQualifiedPublicClassName(record)}({ownedHandle});");
    }
}
