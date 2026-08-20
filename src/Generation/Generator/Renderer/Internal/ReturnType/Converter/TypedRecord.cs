namespace Generator.Renderer.Internal.ReturnType;

internal class TypedRecord : ReturnTypeConverter
{
    public bool Supports(GirModel.ReturnType returnType)
    {
        return returnType.AnyTypeReference.References<GirModel.Record>(out var record) && Model.Record.IsTyped(record);
    }

    public RenderableReturnType Convert(GirModel.ReturnType returnType)
    {
        var type = (GirModel.Record) returnType.AnyTypeReference.AsT0.Type;

        var typeName = returnType switch
        {
            { Transfer: GirModel.Transfer.Full } => Model.TypedRecord.GetFullyQuallifiedOwnedHandle(type),
            _ => Model.TypedRecord.GetFullyQuallifiedUnownedHandle(type)
        };

        //Returned SafeHandles are never "null" but "invalid" in case of C NULL.
        return new RenderableReturnType(typeName);
    }
}
