namespace Generator.Renderer.Internal.ReturnType;

internal class OpaqueUntypedRecord : ReturnTypeConverter
{
    public bool Supports(GirModel.ReturnType returnType)
    {
        return returnType.AnyTypeReference.References<GirModel.Record>(out var record) && Model.Record.IsOpaqueUntyped(record);
    }

    public RenderableReturnType Convert(GirModel.ReturnType returnType)
    {
        var type = (GirModel.Record) returnType.AnyTypeReference.AsT0.Type;

        var typeName = returnType switch
        {
            { Transfer: GirModel.Transfer.Full } => Model.OpaqueTypedRecord.GetFullyQuallifiedOwnedHandle(type),
            _ => Model.OpaqueTypedRecord.GetFullyQuallifiedUnownedHandle(type)
        };

        //Returned SafeHandles are never "null" but "invalid" in case of C NULL.
        return new RenderableReturnType(typeName);
    }
}
