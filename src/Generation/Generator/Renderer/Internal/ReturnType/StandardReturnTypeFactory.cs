using Generator.Model;

namespace Generator.Renderer.Internal;
//TODO: Consider Removing all "Standard" model classes as it is not clear in which cases they are used explicitly and replace them with concrete implementations

internal static class StandardReturnTypeFactory
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
