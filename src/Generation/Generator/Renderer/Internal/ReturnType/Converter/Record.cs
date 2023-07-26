namespace Generator.Renderer.Internal.ReturnType;

internal class Record : ReturnTypeConverter
{
    public bool Supports(GirModel.ReturnType returnType)
    {
        return returnType.AnyType.Is<GirModel.Record>(out var record) && !Model.Record.IsOpaqueTyped(record);
    }

    public RenderableReturnType Convert(GirModel.ReturnType returnType)
    {
        var type = (GirModel.Record) returnType.AnyType.AsT0;

        var nullableTypeName = !returnType.IsPointer
            ? Model.Record.GetFullyQualifiedInternalStructName(type)
            : returnType.Transfer == GirModel.Transfer.None
                ? Model.Record.GetFullyQualifiedInternalUnownedHandle(type)
                : Model.Record.GetFullyQualifiedInternalOwnedHandle(type);

        return new RenderableReturnType(nullableTypeName);
    }
}
