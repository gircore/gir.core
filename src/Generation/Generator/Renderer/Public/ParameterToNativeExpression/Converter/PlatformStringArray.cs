using System;
using System.Collections.Generic;

namespace Generator.Renderer.Public.ParameterToNativeExpressions;

internal class PlatformStringArray : ToNativeParameterConverter
{
    public bool Supports(GirModel.AnyType type)
        => type.IsArray<GirModel.PlatformString>();

    public void Initialize(ParameterToNativeData parameter, IEnumerable<ParameterToNativeData> _)
    {
        var arrayType = parameter.Parameter.AnyTypeOrVarArgs.AsT0.AsT1;

        if (arrayType.IsZeroTerminated)
            NullTerminatedArray(parameter);
        else if (arrayType.Length is not null)
            SizeBasedArray(parameter);
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
                throw new Exception($"Unsupported direction {parameter.Parameter.Direction} for platform string array.");
        }
    }

    private static void NullTerminatedArrayIn(ParameterToNativeData parameter)
    {
        var signatureName = Model.Parameter.GetName(parameter.Parameter);
        var nativeVariableName = signatureName + "Native";

        var createExpression = parameter.Parameter switch
        {
            { Transfer: GirModel.Transfer.Full } => $"{Model.PlatformStringArray.GetInternalUnownedHandleName()}.Create({signatureName})",
            { Transfer: GirModel.Transfer.None } => $"{Model.PlatformStringArray.GetInternalOwnedHandleName()}.Create({signatureName})",
            _ => throw new Exception("Unknown transfer type for parameter with a null terminated platform string array")
        };

        if (parameter.Parameter.Nullable)
            createExpression = $"{signatureName}  is null ? {Model.PlatformStringArray.GetInternalUnownedHandleName()}.NullHandle : {createExpression}";

        var expressionType = parameter.Parameter switch
        {
            { Transfer: GirModel.Transfer.Full } => Model.PlatformStringArray.GetInternalUnownedHandleName(),
            { Transfer: GirModel.Transfer.None } => Model.PlatformStringArray.GetInternalHandleName(),
            _ => throw new Exception("Unknown transfer type for parameter with a null terminated input platform string array")
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
            _ => throw new Exception("Unknown transfer type for parameter with a null terminated output platform string array")
        };

        if (!parameter.Parameter.Nullable)
            createExpression += " ?? throw new System.Exception(\"Unexpected null value\")";

        parameter.SetSignatureName(() => signatureName);
        parameter.SetCallName(() => $"out var {nativeVariableName}");
        parameter.SetPostCallExpression(() => $"{signatureName} = {createExpression};");
    }

    private static void SizeBasedArray(ParameterToNativeData parameter)
    {
        if (parameter.Parameter.Direction != GirModel.Direction.In)
            throw new NotImplementedException($"{parameter.Parameter.AnyTypeOrVarArgs}: Platform string array type with direction != in not yet supported");

        //We don't need any conversion for string[]
        var variableName = Model.Parameter.GetName(parameter.Parameter);
        parameter.SetSignatureName(() => variableName);
        parameter.SetCallName(() => variableName);
    }
}
