namespace Generator.Renderer.Public;

internal static class ReturnType
{
    public static string Render(GirModel.ReturnType returnType)
    {
        return returnType
            .Map(RenderableReturnTypeFactory.Create)
            .NullableTypeName;
    }
}
