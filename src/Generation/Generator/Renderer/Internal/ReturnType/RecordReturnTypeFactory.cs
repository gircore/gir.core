using Generator.Model;

namespace Generator.Renderer.Internal;

internal static class RecordReturnTypeFactory
{
    public static RenderableReturnType Create(GirModel.ReturnType returnType)
    {
        var type = (GirModel.Record) returnType.AnyType.AsT0;

        var nullableTypeName = !returnType.IsPointer
            ? Record.GetFullyQualifiedInternalStructName(type)
            : returnType.Transfer == GirModel.Transfer.None
                ? Record.GetFullyQualifiedInternalUnownedHandle(type)
                : Record.GetFullyQualifiedInternalOwnedHandle(type);

        return new RenderableReturnType(nullableTypeName);
    }
}
