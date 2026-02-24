using System;
using System.Collections.Generic;
using System.Linq;

namespace Generator.Renderer.Public.ParameterToNativeExpressions;

internal class Utf8StringArray : ToNativeParameterConverter
{
    public bool Supports(GirModel.AnyType type)
        => type.IsArray<GirModel.Utf8String>();

    public void Initialize(ParameterToNativeData parameter, IEnumerable<ParameterToNativeData> allParameters)
    {
        var arrayType = parameter.Parameter.AnyTypeOrVarArgs.AsT0.AsT1;

        if (arrayType.IsZeroTerminated)
            NullTerminatedArray(parameter);
        else if (arrayType.Length is not null)
            SizeBasedArray(parameter, allParameters);
        else
            throw new Exception("Unknown kind of array");
    }

    private static void NullTerminatedArray(ParameterToNativeData parameter)
    {
        switch (parameter.Parameter.Direction)
        {
            case GirModel.Direction.In:
                NullTerminatedArrayIn(parameter);
                break;
            case GirModel.Direction.Out:
                NullTerminatedArrayOut(parameter);
                break;
            default:
                throw new Exception($"Unsupported direction {parameter.Parameter.Direction} for utf8 string array.");
        }
    }

    private static void NullTerminatedArrayIn(ParameterToNativeData parameter)
    {
        var signatureName = Model.Parameter.GetName(parameter.Parameter);
        var nativeVariableName = signatureName + "Native";

        var createExpression = parameter.Parameter switch
        {
            { Transfer: GirModel.Transfer.Full } => $"{Model.Utf8StringArray.NullTerminated.GetInternalUnownedHandleName()}.Create({signatureName})",
            { Transfer: GirModel.Transfer.None } => $"{Model.Utf8StringArray.NullTerminated.GetInternalOwnedHandleName()}.Create({signatureName})",
            _ => throw new Exception("Unknown transfer type for parameter with a null terminated input utf8 string array")
        };

        if (parameter.Parameter.Nullable)
            createExpression = $"{signatureName}  is null ? {Model.Utf8StringArray.NullTerminated.GetInternalUnownedHandleName()}.NullHandle : {createExpression}";

        var expressionType = parameter.Parameter switch
        {
            { Transfer: GirModel.Transfer.Full } => Model.Utf8StringArray.NullTerminated.GetInternalUnownedHandleName(),
            { Transfer: GirModel.Transfer.None } => Model.Utf8StringArray.NullTerminated.GetInternalHandleName(),
            _ => throw new Exception("Unknown transfer type for parameter with a null terminated input utf8 string array")
        };

        parameter.SetSignatureName(() => signatureName);
        parameter.SetCallName(() => nativeVariableName);
        parameter.SetExpression(() => $"{expressionType} {nativeVariableName} = {createExpression};");
    }

    private static void NullTerminatedArrayOut(ParameterToNativeData parameter)
    {
        var signatureName = Model.Parameter.GetName(parameter.Parameter);
        var nativeVariableName = signatureName + "Native";

        var createExpression = parameter.Parameter switch
        {
            { Transfer: GirModel.Transfer.Full } => $"{nativeVariableName}.ConvertToStringArray()",
            { Transfer: GirModel.Transfer.None } => $"{nativeVariableName}.ConvertToStringArray()",
            { Transfer: GirModel.Transfer.Container } => $"{nativeVariableName}.ConvertToStringArray()",
            _ => throw new Exception("Unknown transfer type for parameter with a null terminated output utf8 string array")
        };

        if (!parameter.Parameter.Nullable)
            createExpression += " ?? throw new System.Exception(\"Unexpected null value\")";

        parameter.SetSignatureName(() => signatureName);
        parameter.SetCallName(() => $"out var {nativeVariableName}");
        parameter.SetPostCallExpression(() => $"{signatureName} = {createExpression};");
    }

    private static void SizeBasedArray(ParameterToNativeData parameter, IEnumerable<ParameterToNativeData> allParameters)
    {
        switch (parameter.Parameter.Direction)
        {
            case GirModel.Direction.In:
                SizeBasedArrayIn(parameter, allParameters);
                break;
            case GirModel.Direction.Out:
                SizeBasedArrayOut(parameter, allParameters);
                break;
            case GirModel.Direction.InOut:
                SizeBasedArrayInOut(parameter, allParameters);
                break;
            default:
                throw new Exception($"Unsupported direction {parameter.Parameter.Direction} for utf8 string array.");
        }
    }

    private static void SizeBasedArrayIn(ParameterToNativeData parameter, IEnumerable<ParameterToNativeData> allParameters)
    {
        var signatureName = Model.Parameter.GetName(parameter.Parameter);
        var nativeVariableName = signatureName + "Native";

        var createExpression = parameter.Parameter switch
        {
            { Transfer: GirModel.Transfer.Full } => $"{Model.Utf8StringArray.Sized.GetInternalUnownedHandleName()}.Create({signatureName})",
            { Transfer: GirModel.Transfer.None } => $"{Model.Utf8StringArray.Sized.GetInternalOwnedHandleName()}.Create({signatureName})",
            _ => throw new Exception("Unknown transfer type for parameter with sized in utf8 string array")
        };

        if (parameter.Parameter.Nullable)
            createExpression = $"{signatureName}  is null ? (({Model.Utf8StringArray.Sized.GetInternalHandleName()}){Model.Utf8StringArray.Sized.GetInternalUnownedHandleName()}.NullHandle) : {createExpression}";

        parameter.SetSignatureName(() => signatureName);
        parameter.SetCallName(() => nativeVariableName);
        parameter.SetExpression(() => $"var {nativeVariableName} = {createExpression};");

        var lengthIndex = parameter.Parameter.AnyTypeOrVarArgs.AsT0.AsT1.Length ?? throw new Exception("Length missing");
        var lengthParameter = allParameters.ElementAt(lengthIndex);
        var lengthParameterType = Model.Type.GetName(lengthParameter.Parameter.AnyTypeOrVarArgs.AsT0.AsT0);
        lengthParameter.IsArrayLengthParameter = true;
        lengthParameter.SetCallName(() => $"({lengthParameterType}) {nativeVariableName}.Size");
    }

    private static void SizeBasedArrayOut(ParameterToNativeData parameter, IEnumerable<ParameterToNativeData> allParameters)
    {
        var signatureName = Model.Parameter.GetName(parameter.Parameter);
        var nativeVariableName = signatureName + "Native";

        parameter.SetSignatureName(() => signatureName);
        parameter.SetCallName(() => $"out var {nativeVariableName}");
        parameter.SetPostCallExpression(() => parameter.Parameter.Nullable switch
        {
            true => $"{signatureName} = {nativeVariableName}.ConvertToStringArray();",
            false => $"""{signatureName} = {nativeVariableName}.ConvertToStringArray() ?? throw new NullReferenceException("Unexpected null value");"""
        });

        var lengthIndex = parameter.Parameter.AnyTypeOrVarArgs.AsT0.AsT1.Length ?? throw new Exception("Length missing");
        var lengthParameter = allParameters.ElementAt(lengthIndex);

        lengthParameter.IsArrayLengthParameter = true;
        lengthParameter.SetCallName(() => $"out var {lengthParameter.Parameter.Name}");
        lengthParameter.SetPostCallExpression(() => $"{nativeVariableName}.Size = (int) {lengthParameter.Parameter.Name};");
    }

    private static void SizeBasedArrayInOut(ParameterToNativeData parameter, IEnumerable<ParameterToNativeData> allParameters)
    {
        if (parameter.Parameter.Transfer != GirModel.Transfer.Full)
            throw new Exception("Only full transfer supported for inout sized utf8 string arrays");

        var signatureName = Model.Parameter.GetName(parameter.Parameter);
        var nativeVariableName = signatureName + "Native";

        parameter.SetSignatureName(() => signatureName);
        parameter.SetCallName(() => $"ref {nativeVariableName}");
        parameter.SetExpression(() => parameter.Parameter.Nullable switch
        {
            false => $"var {nativeVariableName} = {Model.Utf8StringArray.Sized.GetInternalOwnedHandleName()}.Create({signatureName});",
            true => $"var {nativeVariableName} = {Model.Utf8StringArray.Sized.GetInternalOwnedHandleName()}.Create({signatureName} ?? Array.Empty<string>());"
        });
        parameter.SetPostCallExpression(() => parameter.Parameter.Nullable switch
        {
            false => $"{signatureName} = {nativeVariableName}.ConvertToStringArray() ?? throw new NullReferenceException(\"Unexpected null value\");",
            true => $"""{signatureName} = {nativeVariableName}.ConvertToStringArray();"""
        });

        var lengthIndex = parameter.Parameter.AnyTypeOrVarArgs.AsT0.AsT1.Length ?? throw new Exception("Length missing");
        var lengthParameter = allParameters.ElementAt(lengthIndex);

        lengthParameter.IsArrayLengthParameter = true;
        var lengthParameterType = Model.Type.GetName(lengthParameter.Parameter.AnyTypeOrVarArgs.AsT0.AsT0);
        lengthParameter.SetExpression(() => parameter.Parameter.Nullable switch
        {
            false => $"{lengthParameterType} counterNative = ({lengthParameterType}){signatureName}.Length;",
            true => $"{lengthParameterType} counterNative = ({lengthParameterType}?){signatureName}?.Length ?? 0;"
        });
        lengthParameter.SetCallName(() => "ref counterNative");
        lengthParameter.SetPostCallExpression(() => $"{nativeVariableName}.Size = counterNative;");
    }
}
