using System;
using System.Collections.Generic;

namespace Generator.Renderer.Internal.ParameterToManagedExpressions;

internal class PlatformStringArray : ToManagedParameterConverter
{
    public bool Supports(GirModel.AnyType type)
        => type.IsArray<GirModel.PlatformString>();

    public void Initialize(ParameterToManagedData parameterData, IEnumerable<ParameterToManagedData> parameters)
    {
        var arrayType = parameterData.Parameter.AnyTypeOrVarArgs.AsT0.AsT1;

        if (arrayType.IsZeroTerminated)
            NullTerminatedArray(parameterData);
        else if (arrayType.Length is not null)
            SizeBasedArray(parameterData);
        else
            throw new Exception("Unknown kind of array");
    }

    private static void NullTerminatedArray(ParameterToManagedData parameterData)
    {
        var signatureName = Model.Parameter.GetName(parameterData.Parameter);
        var callName = Model.Parameter.GetConvertedName(parameterData.Parameter);

        var handleExpression = parameterData.Parameter switch
        {
            { Transfer: GirModel.Transfer.None } => $"new {Model.PlatformStringArray.NullTerminated.GetInternalUnownedHandleName()}({signatureName}).ConvertToStringArray()",
            { Transfer: GirModel.Transfer.Full } => $"new {Model.PlatformStringArray.NullTerminated.GetInternalOwnedHandleName()}({signatureName}).ConvertToStringArray()",
            _ => throw new Exception("Unknown transfer type for platform string array to managed expression")
        };

        parameterData.SetSignatureName(() => signatureName);
        parameterData.SetExpression(() => $"var {callName} = {handleExpression};");
        parameterData.SetCallName(() => callName);
    }

    private static void SizeBasedArray(ParameterToManagedData parameterData)
    {
        var variableName = Model.Parameter.GetName(parameterData.Parameter);

        parameterData.SetSignatureName(() => variableName);
        parameterData.SetCallName(() => variableName);
    }
}
