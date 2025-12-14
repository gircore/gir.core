using System;

namespace Generator.Renderer.Internal.Parameter;

internal class PlatformStringArray : ParameterConverter
{
    public bool Supports(GirModel.AnyType anyType)
    {
        return anyType.IsArray<GirModel.PlatformString>();
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
        return parameter.Direction switch
        {
            GirModel.Direction.In => SizeBasedArrayIn(parameter),
            GirModel.Direction.Out => SizeBasedArrayOut(parameter),
            GirModel.Direction.InOut => SizeBasedArrayInOut(parameter),
            _ => throw new Exception("Unknown direction for parameter of a size based platform string array")
        };
    }

    private static RenderableParameter SizeBasedArrayIn(GirModel.Parameter parameter)
    {
        var nullableTypeName = parameter switch
        {
            { Transfer: GirModel.Transfer.None } => Model.PlatformStringArray.Sized.GetInternalHandleName(),
            { Transfer: GirModel.Transfer.Full } => Model.PlatformStringArray.Sized.GetInternalHandleName(),
            { Transfer: GirModel.Transfer.Container } => throw new Exception("Transfer container not supported for platform string arrays"),
            _ => throw new Exception("Can't detect typename for sized platform string array")
        };

        return new RenderableParameter(
            Attribute: string.Empty,
            Direction: ParameterDirection.In(),
            NullableTypeName: nullableTypeName,
            Name: Model.Parameter.GetName(parameter)
        );
    }

    private static RenderableParameter SizeBasedArrayOut(GirModel.Parameter parameter)
    {
        var nullableTypeName = parameter switch
        {
            { Transfer: GirModel.Transfer.None } => Model.PlatformStringArray.Sized.GetInternalUnownedHandleName(),
            { Transfer: GirModel.Transfer.Full } => Model.PlatformStringArray.Sized.GetInternalOwnedHandleName(),
            { Transfer: GirModel.Transfer.Container } => throw new Exception("Transfer container not supported for platform string arrays"),
            _ => throw new Exception("Can't detect typename for sized platform string array")
        };

        return new RenderableParameter(
            Attribute: string.Empty,
            Direction: ParameterDirection.Out(),
            NullableTypeName: nullableTypeName,
            Name: Model.Parameter.GetName(parameter)
        );
    }

    private static RenderableParameter SizeBasedArrayInOut(GirModel.Parameter parameter)
    {
        var nullableTypeName = parameter switch
        {
            { Transfer: GirModel.Transfer.None } => throw new Exception("Transfer none not supported for inout platform string arrays"),
            { Transfer: GirModel.Transfer.Full } => Model.PlatformStringArray.Sized.GetInternalOwnedHandleName(),
            { Transfer: GirModel.Transfer.Container } => throw new Exception("Transfer container not supported for platform string arrays"),
            _ => throw new Exception("Can't detect typename for sized platform string array")
        };

        return new RenderableParameter(
            Attribute: string.Empty,
            Direction: ParameterDirection.Ref(),
            NullableTypeName: nullableTypeName,
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
            { Transfer: GirModel.Transfer.None } => Model.PlatformStringArray.NullTerminated.GetInternalHandleName(),
            { Transfer: GirModel.Transfer.Full } => Model.PlatformStringArray.NullTerminated.GetInternalUnownedHandleName(),
            { Transfer: GirModel.Transfer.Container } => throw new Exception("Transfer container not supported for platform string arrays"),
            _ => throw new Exception("Can't detect typename for platform string array")
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
            { Transfer: GirModel.Transfer.None } => Model.PlatformStringArray.NullTerminated.GetInternalUnownedHandleName(),
            { Transfer: GirModel.Transfer.Full } => Model.PlatformStringArray.NullTerminated.GetInternalOwnedHandleName(),
            { Transfer: GirModel.Transfer.Container } => Model.PlatformStringArray.NullTerminated.GetInternalContainerHandleName(),
            _ => throw new Exception("Can't detect typename for platform string array")
        };

        return new RenderableParameter(
            Attribute: string.Empty,
            Direction: ParameterDirection.Out(),
            NullableTypeName: nullableTypeName,
            Name: Model.Parameter.GetName(parameter)
        );
    }
}
