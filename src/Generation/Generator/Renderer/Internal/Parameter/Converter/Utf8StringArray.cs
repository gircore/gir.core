using System;

namespace Generator.Renderer.Internal.Parameter;

internal class Utf8StringArray : ParameterConverter
{
    public bool Supports(GirModel.AnyType anyType)
    {
        return anyType.IsArray<GirModel.Utf8String>();
    }

    public RenderableParameter Convert(GirModel.Parameter parameter)
    {
        return parameter.AnyTypeOrVarArgs.AsT0.AsT1.Length is null
            ? ParameterWithoutLength(parameter)
            : ParameterWithLength(parameter);
    }

    private static RenderableParameter ParameterWithLength(GirModel.Parameter parameter)
    {
        var length = parameter.AnyTypeOrVarArgs.AsT0.AsT1.Length ?? throw new Exception("Length must not be null");

        return new RenderableParameter(
            Attribute: MarshalAs.UnmanagedLpArray(sizeParamIndex: length),
            Direction: string.Empty,
            NullableTypeName: Model.ArrayType.GetName(parameter.AnyTypeOrVarArgs.AsT0.AsT1),
            Name: Model.Parameter.GetName(parameter)
        );
    }

    private static RenderableParameter ParameterWithoutLength(GirModel.Parameter parameter)
    {
        return parameter.Direction switch
        {
            GirModel.Direction.In => ParameterWithoutLengthIn(parameter),
            GirModel.Direction.Out => ParameterWithoutLengthOut(parameter),
            _ => throw new Exception("Unknown direction for parameter")
        };
    }

    private static RenderableParameter ParameterWithoutLengthIn(GirModel.Parameter parameter)
    {
        if (parameter.AnyTypeOrVarArgs.AsT0.AsT1.Length is not null)
            throw new Exception("Length must be null");

        if (parameter.Direction != GirModel.Direction.In)
            throw new Exception("Direction must be in");

        var nullableTypeName = parameter switch
        {
            { Transfer: GirModel.Transfer.None } => Model.Utf8StringArray.GetInternalHandleName(),
            { Transfer: GirModel.Transfer.Full } => Model.Utf8StringArray.GetInternalUnownedHandleName(),
            { Transfer: GirModel.Transfer.Container } => throw new Exception("Transfer container not supported for utf8 string arrays"),
            _ => throw new Exception("Can't detect typename for utf8 string array")
        };

        return new RenderableParameter(
            Attribute: string.Empty,
            Direction: ParameterDirection.In(),
            NullableTypeName: nullableTypeName,
            Name: Model.Parameter.GetName(parameter)
        );
    }

    private static RenderableParameter ParameterWithoutLengthOut(GirModel.Parameter parameter)
    {
        if (parameter.AnyTypeOrVarArgs.AsT0.AsT1.Length is not null)
            throw new Exception("Length must be null");

        if (parameter.Direction != GirModel.Direction.Out)
            throw new Exception("Direction must be out");

        var nullableTypeName = parameter switch
        {
            { Transfer: GirModel.Transfer.None } => Model.Utf8StringArray.GetInternalUnownedHandleName(),
            { Transfer: GirModel.Transfer.Full } => Model.Utf8StringArray.GetInternalOwnedHandleName(),
            { Transfer: GirModel.Transfer.Container } => Model.Utf8StringArray.GetInternalContainerHandleName(),
            _ => throw new Exception("Can't detect typename for utf8 string array")
        };

        return new RenderableParameter(
            Attribute: string.Empty,
            Direction: ParameterDirection.Out(),
            NullableTypeName: nullableTypeName,
            Name: Model.Parameter.GetName(parameter)
        );
    }
}
