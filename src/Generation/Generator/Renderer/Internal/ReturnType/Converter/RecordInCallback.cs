namespace Generator.Renderer.Internal.ReturnType;

internal class RecordInCallback : ReturnTypeConverter
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
            : Model.Record.GetFullyQualifiedInternalHandle(type);

        return new RenderableReturnType(nullableTypeName);
    }
}
