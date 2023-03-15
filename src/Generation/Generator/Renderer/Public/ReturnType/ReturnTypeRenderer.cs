using Generator.Renderer.Public.ReturnType;

namespace Generator.Renderer.Public;

internal static class ReturnTypeRenderer
{
    public static string Render(GirModel.ReturnType returnType)
    {
        return returnType
            .Map(RenderableReturnTypeFactory.Create)
            .NullableTypeName;
    }
}
