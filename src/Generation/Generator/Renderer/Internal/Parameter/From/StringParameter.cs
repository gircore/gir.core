using Generator.Model;

namespace Generator.Renderer.Internal;

internal static class StringParameterFactory
{
    public static RenderableParameter Create(GirModel.Parameter parameter)
    {
        return new RenderableParameter(
            Attribute: GetAttribute(parameter),
            Direction: GetDirection(parameter),
            NullableTypeName: GetNullableTypeName(parameter),
            Name: Parameter.GetName(parameter)
        );
    }

    private static string GetAttribute(GirModel.Parameter parameter) => parameter.AnyType.AsT0 switch
    {
        // Marshal as a UTF-8 encoded string
        GirModel.Utf8String => MarshalAs.UnmanagedLpUtf8String(),

        // Marshal as a null-terminated array of ANSI characters
        // TODO: This is likely incorrect:
        //  - GObject introspection specifies that Windows should use
        //    UTF-8 and Unix should use ANSI. Does using ANSI for
        //    everything cause problems here?
        GirModel.PlatformString => MarshalAs.UnmanagedLpString(),

        _ => ""
    };

    private static string GetNullableTypeName(GirModel.Parameter parameter)
        => Type.GetName(parameter.AnyType.AsT0) + Nullable.Render(parameter);

    private static string GetDirection(GirModel.Parameter parameter) => parameter switch
    {
        { Direction: GirModel.Direction.InOut } => ParameterDirection.Ref(),
        { Direction: GirModel.Direction.Out, CallerAllocates: true } => ParameterDirection.Ref(),
        { Direction: GirModel.Direction.Out } => ParameterDirection.Out(),
        _ => ParameterDirection.In()
    };
}
