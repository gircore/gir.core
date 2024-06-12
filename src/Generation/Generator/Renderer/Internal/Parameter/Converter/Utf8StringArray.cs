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
        var arrayType = parameter.AnyTypeOrVarArgs.AsT0.AsT1;

        if (arrayType.IsZeroTerminated)
            return NullTerminatedArray(parameter);

        if (arrayType.Length is not null)
            return SizeBasedArray(parameter);

        throw new Exception("Unknown kind of array");
    }

    private static RenderableParameter SizeBasedArray(GirModel.Parameter parameter)
    {
        var length = parameter.AnyTypeOrVarArgs.AsT0.AsT1.Length ?? throw new Exception("Length must not be null");
        var typeName = Model.ArrayType.GetName(parameter.AnyTypeOrVarArgs.AsT0.AsT1) + Nullable.Render(parameter);

        return new RenderableParameter(
            Attribute: MarshalAs.UnmanagedLpArray(sizeParamIndex: length),
            Direction: string.Empty,
            NullableTypeName: typeName,
            Name: Model.Parameter.GetName(parameter)
        );
    }

    private static RenderableParameter NullTerminatedArray(GirModel.Parameter parameter)
    {
        return parameter.Direction switch
        {
            GirModel.Direction.In => NullTerminatedArrayIn(parameter),
            GirModel.Direction.Out => NullTerminatedArrayOut(parameter),
            _ => throw new Exception("Unknown direction for parameter")
        };
    }

    private static RenderableParameter NullTerminatedArrayIn(GirModel.Parameter parameter)
    {
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

    private static RenderableParameter NullTerminatedArrayOut(GirModel.Parameter parameter)
    {
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
