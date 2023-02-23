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
        // TODO the type name should depend on the transfer / direction / caller-allocates flags.
        { AnyType.AsT0: GirModel.PlatformString, Nullable: true } => PlatformString.GetInternalNullableHandleName(),
        { AnyType.AsT0: GirModel.PlatformString, Nullable: false } => PlatformString.GetInternalNonNullableHandleName(),
        { AnyType.AsT0: GirModel.Utf8String, Nullable: true } => Utf8String.GetInternalNullableHandleName(),
        _ => Utf8String.GetInternalNonNullableHandleName(),
    };

    private static string GetDirection(GirModel.Parameter parameter) => parameter switch
    {
        { Direction: GirModel.Direction.InOut } => ParameterDirection.Ref(),
        { Direction: GirModel.Direction.Out, CallerAllocates: true } => ParameterDirection.Ref(),
        { Direction: GirModel.Direction.Out } => ParameterDirection.Out(),
        _ => ParameterDirection.In()
    };
}
