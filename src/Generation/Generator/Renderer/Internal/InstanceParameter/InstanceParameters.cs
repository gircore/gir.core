namespace Generator.Renderer.Internal;

internal static class InstanceParameters
{
    public static string Render(GirModel.InstanceParameter parameter)
    {
        return parameter
            .Map(RenderableInstanceParameterFactory.Create)
            .Map(Render);
    }

    private static string Render(RenderableInstanceParameter parameter)
        => $@"{parameter.NullableTypeName} {parameter.Name}";
}
