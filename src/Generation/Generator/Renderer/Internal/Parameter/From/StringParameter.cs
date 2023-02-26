using System;
using Generator.Model;

namespace Generator.Renderer.Internal;

internal static class StringParameterFactory
{
    public static RenderableParameter Create(GirModel.Parameter parameter)
    {
        return new RenderableParameter(
            Attribute: string.Empty,
            Direction: GetDirection(parameter),
            NullableTypeName: GetNullableTypeName(parameter),
            Name: Parameter.GetName(parameter)
        );
    }

    private static string GetNullableTypeName(GirModel.Parameter parameter) => parameter switch
    {
        // Note: optional parameters are generated as regular out parameters, which the caller can ignore with 'out var _' if desired.
        { AnyType.AsT0: GirModel.PlatformString, Nullable: true, Direction: GirModel.Direction.In } => PlatformString.GetInternalNullableHandleName(),
        { AnyType.AsT0: GirModel.PlatformString, Nullable: false, Direction: GirModel.Direction.In } => PlatformString.GetInternalNonNullableHandleName(),
        { AnyType.AsT0: GirModel.PlatformString, Nullable: true, Transfer: GirModel.Transfer.Full } => PlatformString.GetInternalNullableOwnedHandleName(),
        { AnyType.AsT0: GirModel.PlatformString, Nullable: true, Transfer: GirModel.Transfer.None } => PlatformString.GetInternalNullableUnownedHandleName(),
        { AnyType.AsT0: GirModel.PlatformString, Nullable: false, Transfer: GirModel.Transfer.Full } => PlatformString.GetInternalNonNullableOwnedHandleName(),
        { AnyType.AsT0: GirModel.PlatformString, Nullable: false, Transfer: GirModel.Transfer.None } => PlatformString.GetInternalNonNullableUnownedHandleName(),
        { AnyType.AsT0: GirModel.Utf8String, Nullable: true, Direction: GirModel.Direction.In } => Utf8String.GetInternalNullableHandleName(),
        { AnyType.AsT0: GirModel.Utf8String, Nullable: false, Direction: GirModel.Direction.In } => Utf8String.GetInternalNonNullableHandleName(),
        { AnyType.AsT0: GirModel.Utf8String, Nullable: true, Transfer: GirModel.Transfer.Full } => Utf8String.GetInternalNullableOwnedHandleName(),
        { AnyType.AsT0: GirModel.Utf8String, Nullable: true, Transfer: GirModel.Transfer.None } => Utf8String.GetInternalNullableUnownedHandleName(),
        { AnyType.AsT0: GirModel.Utf8String, Nullable: false, Transfer: GirModel.Transfer.Full } => Utf8String.GetInternalNonNullableOwnedHandleName(),
        { AnyType.AsT0: GirModel.Utf8String, Nullable: false, Transfer: GirModel.Transfer.None } => Utf8String.GetInternalNonNullableUnownedHandleName(),
        _ => throw new NotImplementedException($"{parameter.Name}: Unknown string parameter type")
    };

    private static string GetDirection(GirModel.Parameter parameter) => parameter switch
    {
        { Direction: GirModel.Direction.InOut } => ParameterDirection.Ref(),
        { Direction: GirModel.Direction.Out, CallerAllocates: true } => ParameterDirection.Ref(),
        { Direction: GirModel.Direction.Out } => ParameterDirection.Out(),
        _ => ParameterDirection.In()
    };
}
