using Generator.Model;

namespace Generator.Renderer.Internal;

internal static class Utf8StringReturnTypeFactory
{
    public static RenderableReturnType Create(GirModel.ReturnType returnType)
    {
        var nullableTypeName = returnType switch
        {
            // Return values which return a string without transferring ownership to us can not be marshalled automatically
            // as the marshaller want's to free the unmanaged memory which is not allowed if the ownership is not transferred
            { Transfer: GirModel.Transfer.None } => Type.Pointer,
            _ => Type.GetName(returnType.AnyType.AsT0) + Nullable.Render((GirModel.Nullable) returnType)
        };

        return new RenderableReturnType(nullableTypeName);
    }
}
