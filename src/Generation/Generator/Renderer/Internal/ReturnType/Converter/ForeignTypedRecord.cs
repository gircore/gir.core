namespace Generator.Renderer.Internal.ReturnType;

internal class ForeignTypedRecord : ReturnTypeConverter
{
    public bool Supports(GirModel.ReturnType returnType)
    {
        return returnType.AnyTypeReference.References<GirModel.Record>(out var record) && Model.Record.IsForeignTyped(record);
    }

    public RenderableReturnType Convert(GirModel.ReturnType returnType)
    {
        var type = (GirModel.Record) returnType.AnyTypeReference.AsT0.Type;

        var typeName = returnType switch
        {
            { Transfer: GirModel.Transfer.Full } => Model.ForeignTypedRecord.GetFullyQuallifiedOwnedHandle(type),
            _ => Model.ForeignTypedRecord.GetFullyQuallifiedUnownedHandle(type)
        };

        //Returned SafeHandles are never "null" but "invalid" in case of C NULL.
        return new RenderableReturnType(typeName);
    }
}
