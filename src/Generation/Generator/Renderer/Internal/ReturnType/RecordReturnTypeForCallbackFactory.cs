using Generator.Model;

namespace Generator.Renderer.Internal;

internal static class RecordReturnTypeForCallbackFactory
{
    public static RenderableReturnType Create(GirModel.ReturnType returnType)
    {
        var type = (GirModel.Record) returnType.AnyType.AsT0;

        var nullableTypeName = !returnType.IsPointer
            ? Record.GetFullyQualifiedInternalStructName(type)
            : Record.GetFullyQualifiedInternalHandle(type);

        return new RenderableReturnType(nullableTypeName);
    }
}
