namespace Generator.Renderer.Internal;

internal static class ReturnType
{
    public static string Render(GirModel.ReturnType returnType)
    {
        return returnType
            .Map(RenderableReturnTypeFactory.Create)
            .NullableTypeName;
    }

    public static string RenderForCallback(GirModel.ReturnType returnType)
    {
        return returnType
            .Map(RenderableReturnTypeFactory.CreateForCallback)
            .NullableTypeName;
    }
}
