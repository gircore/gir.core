using Generator.Model;

namespace Generator.Renderer.Public;

internal static class StandardReturnType
{
    public static RenderableReturnType Create(GirModel.ReturnType returnType)
    {
        var nullableTypeName = returnType.AnyType.Match(
            type => Type.GetName(type) + Nullable.Render((GirModel.Nullable) returnType),
            arrayType => ArrayType.GetName(arrayType) //TODO: Consider if arrayType should be supported by this class
        );

        return new RenderableReturnType(nullableTypeName);
    }
}
