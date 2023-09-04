namespace Generator.Renderer.Internal.ReturnType;

internal class OpaqueUntypedRecordCallback : ReturnTypeConverter
{
    public bool Supports(GirModel.ReturnType returnType)
    {
        return returnType.AnyType.Is<GirModel.Record>(out var record) && Model.Record.IsOpaqueUntyped(record);
    }

    public RenderableReturnType Convert(GirModel.ReturnType returnType)
    {
        return new RenderableReturnType(Model.Type.Pointer);
    }
}
